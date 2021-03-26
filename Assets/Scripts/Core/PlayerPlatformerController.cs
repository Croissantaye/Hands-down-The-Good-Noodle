using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject
{
    public delegate void OnKillPlayer();
    public static event OnKillPlayer Killed;

    public float maxSpeed = 7f;
    public float jumpTakeoffSpeed = 7f;
    public float shiftModifier = 1.5f;
    public bool canRun = false;

    public int maxHealth = 3;
    private int health;
    public bool isInvincible;
    private HealthSystem playerHealth;
    private GrappleSystem grapple;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color hurtColor = Color.yellow;
    private Color normalColor;

    [SerializeField] private Projectile pfProjectile;
    public Vector2 ropeHook;
    public float swingForce = 47f;
    
    //
    public delegate void FireWeapon();
    public static event FireWeapon Shoot;

    //private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
        grapple = GetComponent<GrappleSystem>();
        // animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        playerHealth = GetComponent<HealthSystem>();
        playerHealth.setAll(maxHealth);
    }

    //temp block
    /*
    private void Shoot(){
        PlayerAimWeapon aim = gameObject.GetComponentInChildren<PlayerAimWeapon>();
        Projectile temp = Instantiate(pfProjectile, aim.getGunPoint().position, Quaternion.identity);
        temp.Setup(aim.getAimDirection());
        temp = null;
    }
    */



    public Rigidbody2D GetPlayerRB(){
        return rb;
    }
    public Vector3 GetRBPosition(){
        return rb.position;
    }

    public void SetCurrenthealth(int h){
        playerHealth.setHealth(h);
    }

    public int GetCurrentHealth(){
        return playerHealth.getHealth();
    }

    public void Hurt(){
        playerHealth.decrement();
        if(gameObject.activeInHierarchy)
            StartCoroutine(hurtEffect());
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeoffSpeed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }
        if(grapple.GetIsRopeOut()){
            move.y = 0f;
            velocity.y = 0f;
        }

        if(Input.GetButtonDown("Fire3") && canRun)
        {
            maxSpeed = (float)maxSpeed * shiftModifier;
        }
        else if(Input.GetButtonUp("Fire3") && canRun)
        {
            maxSpeed = (float)maxSpeed / shiftModifier;
        }

        if(Input.GetMouseButtonDown(0)){
            if (Shoot != null)
            {
                Shoot();
            }
        }

        // bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        // if(flipSprite)
        // {
        //     spriteRenderer.flipX = !spriteRenderer.flipX;
        // }

        // animator.SetBool("grounded", grounded);
        // animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    void OnCollisionEnter2D(Collision2D other) {
        BasicEnemy enemy = other.gameObject.GetComponent<BasicEnemy>();
        //all a bit janky. Physics isn't quite right
        ContactPoint2D hitPoint = other.GetContact(0);
        if(enemy){
            //print("Velocity before " + rb2D.velocity);
            // print(hitPoint.normal);
            // Vector2 bounce = new Vector2(hitPoint.normal.x, .7f);
            // rb2D.velocity = bounce.normalized * 5;
            rb2D.AddForce(hitPoint.normal * 2, ForceMode2D.Impulse);
            //print("Velocity after " + rb2D.velocity);
            float dotResult = Vector2.Dot(hitPoint.normal, Vector2.up);
            bool isOnTop;
            if(dotResult > .5f){
                isOnTop = true;
            }
            else{
                isOnTop = false;
            }
            if(!isInvincible && !isOnTop) {
                playerHealth.decrement();
                print("Player hit. Health: " + playerHealth.getHealth());
                if(playerHealth.getHealth() <= 0){
                    Die();
                }
                else{
                    StartCoroutine(hurtEffect());
                }
            }
        }
    }

    IEnumerator hurtEffect(){
        spriteRenderer.color = hurtColor;
        yield return new WaitForSeconds(.5f);
        spriteRenderer.color = normalColor;
    }

    public void Die(){
        // gameObject.SetActive(false);
        if(Killed != null){
            Killed();
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
