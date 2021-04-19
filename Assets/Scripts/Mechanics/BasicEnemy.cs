using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    // private PolygonCollider2D collision;
    protected SpriteRenderer spriteRenderer;
    protected Color normalColor;
    protected Color hurtColor;
    protected HealthSystem enemyHealth;
    [SerializeField] protected int maxHealth;
    protected int health;
    [SerializeField] protected bool canDie;
    // private Vector2 startPos;
    [SerializeField] [Range (.1f, 20f)] protected float speed;

    // public int unitsMoved;

    // private bool faceLeft;

    // private float leftLimit;
    // private float rightLimit;

    // private Vector2 direction;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // collision = GetComponent<PolygonCollider2D>();
        enemyHealth = GetComponent<HealthSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        normalColor = spriteRenderer.color;
        hurtColor = Color.yellow;

        enemyHealth.setAll(maxHealth);
        health = maxHealth;

        // startPos = transform.position;

        // faceLeft = true;

        // leftLimit = startPos.x - unitsMoved;
        // rightLimit = startPos.x + unitsMoved;

        // direction = Vector2.zero;
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     if (faceLeft)
    //     {
    //         direction = Vector2.left;
    //         if(rb.position.x <= leftLimit)
    //         {
    //             rb.position = new Vector2(leftLimit, rb.position.y);
    //             direction = Vector2.right;
    //             faceLeft = false;
    //         }
    //     }
    //     if (!faceLeft)
    //     {
    //         direction = Vector2.right;
    //         if (rb.position.x >= rightLimit)
    //         {
    //             rb.position = new Vector2(rightLimit, rb.position.y);
    //             direction = Vector2.left;
    //             faceLeft = true;
    //         }
    //     }

    //    //print(faceLeft);
    //    move(direction);
    // }

    protected virtual void move(Vector2 direction)
    {
       rb.position = rb.position + (direction * speed * Time.fixedDeltaTime);
       // Debug.Log(direction);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) 
    {
        // Debug.Log("Enemy triggerEnter2D");
        // Debug.Log(collision.ToString());
    }

    protected virtual bool shouldDieFromCollision(Collision2D collision)
    {
        // PlayerPlatformerController player = collision.gameObject.GetComponent<PlayerPlatformerController>();
        Projectile bullet = collision.gameObject.GetComponent<Projectile>();
        // if(player && collision.GetContact(0).normal.y <= -.5f)
        // {
        //     return true;
        // }
        if(bullet && !bullet.IsEnemyProjectile()){
            bullet.hit();enemyHealth.decrement();
            if(enemyHealth.getHealth() <= 0)
                return true;
        }
        return false;
    }

    private void Die() {
        Debug.Log("enemy died");
        gameObject.SetActive(false);
    }

    public void Hurt(){
        if(canDie){
            enemyHealth.decrement();
            // Debug.Log(enemyHealth.getHealth());
            if(enemyHealth.getHealth() == 0){
                Die();
            }
            else{
                StartCoroutine(hurtEffect());
            }
        }
    }

    IEnumerator hurtEffect(){
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(.5f);
        spriteRenderer.color = normalColor;
    }
}
