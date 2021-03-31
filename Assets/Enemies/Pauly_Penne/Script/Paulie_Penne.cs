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
    [SerializeField] EnemyProjectile bullet;
    private Vector3 gunPointLeft;
    private Vector3 gunpointRight;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int PaulieMaxHealth = 3;
    [SerializeField] [Range (0f, 45f)] private float ConeOfVisionAngle = 15f;
    [SerializeField] [Range (.1f, 100f)] private float RangeOfSight = 25f;
    private ContactFilter2D contactFilter;
    private bool PlayerSeen;

    protected override void Start(){
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<HealthSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gunpoint = GetComponentInChildren<Transform>();
        // Debug.Log(gunpoint.position);

        normalColor = spriteRenderer.color;
        hurtColor = Color.yellow;

        enemyHealth.setAll(PaulieMaxHealth);
        // health = maxHealth;

        canShoot = true;
        gunPointLeft = gunpoint.position - transform.position;
        gunpointRight = transform.position + new Vector3(-gunPointLeft.x, gunPointLeft.y, 0f);

        PlayerSeen = false;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask.GetMask("player"));
        // Debug.Log(LayerMask.GetMask("player"));
        contactFilter.useLayerMask = true;
    }

    void FixedUpdate(){
        // Debug.Log("Gunpoint" + gunpoint.position);
        // Debug.Log("Transform" + transform.position);
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
        int hitNum = Physics2D.Raycast(transform.position, GetDirection(), contactFilter, results, RangeOfSight);
        if(hitNum > 0){
            Debug.Log("player seen");
            Debug.DrawLine(transform.position, results[0].point, Color.blue);
            PlayerSeen = true;
        }
    }    

    private void FlipDirection(){
        if(!spriteRenderer.flipX){
            spriteRenderer.flipX = true;
            gunpoint.position = gunpointRight;
        }
        else if(spriteRenderer.flipX){
            spriteRenderer.flipX = false;
            gunpoint.position = gunPointLeft;
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
            Debug.Log("paulie shoot");
            Projectile temp = Instantiate(bullet, gunpoint.position, Quaternion.identity);
            Vector3 direction = GetDirection();
            temp.Setup(direction, bulletSpeed);
            temp = null;
            StartCoroutine(CoolDown());
        }
    }

    private Vector3 GetDirection(){
        Vector3 direction = Vector3.zero;
        float transform_x = gameObject.GetComponent<Transform>().position.x;
        float gunPoint_x = gunpoint.position.x;
        if(transform_x > gunPoint_x){
            direction = Vector3.left;
        }
        else if(transform_x < gunPoint_x){
            direction = Vector3.right;
        }
        return direction;
    }

    IEnumerator CoolDown(){
        // wait between each shot
        canShoot = false;
        PlayerSeen = false;
        yield return new WaitForSeconds(coolDownTime);
        canShoot = true;
    }
}
