using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject
{
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
    private PlayerAimWeapon Aim;
    private SpriteRenderer spriteRenderer;
    private Color hurtColor = Color.yellow;
    private Color normalColor;

    public Vector2 ropeHook;
    public float swingForce = 47f;
    
    private Weapon currentWeapon;
    private Pistol pistol;
    private Shotgun shotgun;

    public delegate void OnKillPlayer(int health);
    public static event OnKillPlayer Killed;
    public static event OnKillPlayer Hurt;
    public delegate void FireWeapon(int ammo, int maxAmmo);
    public static event FireWeapon Shoot;
    public static event FireWeapon Reload;
    public static event FireWeapon Change;

    //private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
        grapple = GetComponent<GrappleSystem>();
        // animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pistol = GetComponent<Pistol>();
        shotgun = GetComponent<Shotgun>();
        Aim = gameObject.transform.Find("Arm").gameObject.GetComponent<PlayerAimWeapon>();
    }

    void Start() {
        playerHealth = GetComponent<HealthSystem>();
        playerHealth.setAll(maxHealth);
        pistol.enabled = true;
        currentWeapon = pistol;
        shotgun.enabled = false;
        // Debug.Log(shotgun.GetCurrentAmmo());
        // shotgun.enabled = false;
    }

    private void ChangeWeapon(Weapon weapon){
        if(weapon == pistol){
            pistol.enabled = true;
            shotgun.enabled = false;
            currentWeapon = pistol;
            // Debug.Log("Switch to pistol");
        }
        else if(weapon == shotgun){
            shotgun.enabled = true;
            pistol.enabled = false;
            currentWeapon = shotgun;
            // Debug.Log("Switch to shotgun");
        }
        if(Change != null){
            Change(currentWeapon.GetCurrentAmmo(), currentWeapon.GetMaxAmmo());
        }
    }

    public void ResetGrapple(){
        grapple.DestroyRope();
    }
    public void DisableWeapons(){
        Aim.enabled = false;
        pistol.enabled = false;
        shotgun.enabled = false;
    }

    public void EnableWeapons(){
        Aim.enabled = true;
        pistol.enabled = true;
        shotgun.enabled = true;
    }

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

    public HealthSystem GetHealthSystem(){
        return playerHealth;
    }

    public void hurt(){
        playerHealth.decrement();
        if(Hurt != null){
            Hurt(playerHealth.getHealth());
        }
        if(GetCurrentHealth() <= 0)
            Die();
        if(gameObject.activeInHierarchy)
            StartCoroutine(hurtEffect());
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        if(grapple.IsRopeOut()){
            velocity.y = 0;
        }

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeoffSpeed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(grapple.IsRopeOut()){
                velocity.y = jumpTakeoffSpeed;
                grapple.DestroyRope();
            }
            if(velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
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
                Shoot(currentWeapon.GetCurrentAmmo(), currentWeapon.GetMaxAmmo());
            }
        }

        if(Input.GetButtonDown("reload")){
            if(Shoot != null){
                Reload(currentWeapon.GetCurrentAmmo(), currentWeapon.GetMaxAmmo());
            }
        }

        if(Input.GetButtonDown("Num1")){
            ChangeWeapon(pistol);
        }
        else if(Input.GetButtonDown("Num2")){
            ChangeWeapon(shotgun);
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
            // rb2D.AddForce(hitPoint.normal * 2, ForceMode2D.Impulse);
            //print("Velocity after " + rb2D.velocity);
            // float dotResult = Vector2.Dot(hitPoint.normal, Vector2.up);
            // bool isOnTop;
            // if(dotResult > .5f){
            //     isOnTop = true;
            // }
            // else{
            //     isOnTop = false;
            // }
            // if(!isInvincible && !isOnTop) {
            if(!isInvincible){
                playerHealth.decrement();
                // print("Player hit. Health: " + playerHealth.getHealth());
                if(playerHealth.getHealth() <= 0){
                    Die();
                }
                else{
                    if(Hurt != null){
                        Hurt(playerHealth.getHealth());
                    }
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
        if(Killed != null && Hurt != null){
            Killed(playerHealth.getMaxHealth());
            Hurt(playerHealth.getMaxHealth());
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
