using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paulie_Penne : BasicEnemy
{
    // [SerializeField] private BoxCollider2D triggerBox;
    [SerializeField] private BoxCollider2D collisionBox;
    private Animator animator;
    [SerializeField] [Range (.1f, 5f)] float coolDownTime = .5f;
    private bool canShoot;
    private Transform gunpoint;
    [SerializeField] private EnemyProjectile bullet;
    // private Vector3 gunPointLeft;
    // private Vector3 gunpointRight;
    [SerializeField] private bool FaceLeft;
    private FaceDireciton faceDireciton;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int PaulieMaxHealth = 3;
    // [SerializeField] [Range (0f, 45f)] private float ConeOfVisionAngle = 15f;
    [SerializeField] [Range (.1f, 100f)] private float RangeOfSight = 25f;
    private ContactFilter2D contactFilter;
    private bool PlayerSeen;

    protected override void Start(){
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<HealthSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gunpoint = GetComponentInChildren<Transform>();

        normalColor = spriteRenderer.color;
        hurtColor = Color.yellow;

        animator.SetBool("is_shooting", false);

        enemyHealth.setAll(PaulieMaxHealth);

        canShoot = true;
        if(FaceLeft){
            faceDireciton = FaceDireciton.Left;
        }
        else{
            faceDireciton = FaceDireciton.Right;
            FlipDirection();
        }

        PlayerSeen = false;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask.GetMask("player"));
        contactFilter.useLayerMask = true;
    }

    void FixedUpdate(){
        if(canShoot){
            LineOfSight();
        }
        if(PlayerSeen){
            shoot();
        }
    }

    private void LineOfSight(){
        // Debug.Log(GetDirection());
        List<RaycastHit2D> results = new List<RaycastHit2D>(16);
        // Debug.DrawRay(transform.position, GetDirection(), Color.blue, 1f);
        int hitNum = Physics2D.Raycast(transform.position, GetDirection(), contactFilter, results, RangeOfSight);
        Debug.Log(hitNum);
        if(hitNum > 0){
            Debug.Log("player seen");
            // Debug.DrawLine(transform.position, results[0].point, Color.blue, coolDownTime);
            PlayerSeen = true;
            results.Clear();
        }
    }    

    private void FlipDirection(){
        if(!spriteRenderer.flipX){
            spriteRenderer.flipX = true;
            faceDireciton = FaceDireciton.Right;
            // gunpoint.position = gunpointRight;
        }
        else if(spriteRenderer.flipX){
            spriteRenderer.flipX = false;
            faceDireciton = FaceDireciton.Left;
            // gunpoint.position = gunPointLeft;
        }
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
    //     if(player){
    //         shoot();
    //     }
    // }

    private void shoot(){
        if(canShoot && PlayerSeen){
            animator.SetBool("is_shooting", true);
            Debug.Log("paulie shoot");
            Projectile temp = Instantiate(bullet, gunpoint.position, Quaternion.identity);
            Vector3 direction = GetDirection();
            temp.Setup(direction, bulletSpeed);
            temp = null;
            StartCoroutine(CoolDown());
        }
    }

    private Vector3 GetDirection(){
        Vector3 direction;
        if(faceDireciton == FaceDireciton.Left){
            direction = Vector3.left;
        }
        else{
            direction = Vector3.right;
        }
        return direction;
    }

    IEnumerator CoolDown(){
        // wait between each shot
        canShoot = false;
        PlayerSeen = false;
        // float transition = animator.GetAnimatorTransitionInfo(animator.GetLayerIndex("pauly_shoot")).duration;
        // Debug.Log(transition);
        yield return new WaitForSeconds(.25f);
        animator.SetBool("is_shooting", false);
        yield return new WaitForSeconds(coolDownTime - .25f);
        canShoot = true;
    }

    enum FaceDireciton
    {
        Left,
        Right
    }
}
