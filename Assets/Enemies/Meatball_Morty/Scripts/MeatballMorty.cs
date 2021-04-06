using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatballMorty : BasicEnemy
{
    private Rigidbody2D rb;
    private CircleCollider2D collision;
    private float circumference;
    private SpriteRenderer spriteRenderer;
    private Color normalColor;
    private Color hurtColor;
    private HealthSystem enemyHealth;
    public int maxHealth = 3;
    public int health = 3;
    private Vector2 startPos;
    public float speed;

    public int unitsMoved;

    private bool faceLeft;

    private float leftLimit;
    private float rightLimit;

    private Vector2 direction;
    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<CircleCollider2D>();
        enemyHealth = GetComponent<HealthSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        normalColor = spriteRenderer.color;
        hurtColor = Color.yellow;

        enemyHealth.setAll(maxHealth);
        health = maxHealth;

        startPos = transform.position;

        faceLeft = true;

        leftLimit = startPos.x - unitsMoved;
        rightLimit = startPos.x + unitsMoved;

        direction = Vector2.zero;

        circumference = 2 * collision.radius * Mathf.PI;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 oldPos = rb.position;

        if (faceLeft)
        {
            direction = Vector2.left;
            if(rb.position.x <= leftLimit)
            {
                rb.position = new Vector2(leftLimit, rb.position.y);
                direction = Vector2.right;
                faceLeft = false;
                spriteRenderer.flipY = true;
            }
        }
        if (!faceLeft)
        {
            direction = Vector2.right;
            if (rb.position.x >= rightLimit)
            {
                rb.position = new Vector2(rightLimit, rb.position.y);
                direction = Vector2.left;
                faceLeft = true;
                spriteRenderer.flipY = false;
            }
        }

       //print(faceLeft);
       move(direction);
       MortyRoll(oldPos);
    }

    // protected override void move(Vector2 direction)
    // {
    //    rb.position = rb.position + (direction * speed * Time.deltaTime);
    // }

    protected override void OnTriggerEnter2D(Collider2D collider) 
    {
        if(shouldDieFromCollision(collider))
        {
            Die();
        }
    }

    protected override bool shouldDieFromCollision(Collider2D collision)
    {
        PlayerPlatformerController player = collision.gameObject.GetComponent<PlayerPlatformerController>();
        Projectile bullet = collision.gameObject.GetComponent<Projectile>();
        if(bullet && gameObject.activeInHierarchy){
            bullet.hit();
            enemyHealth.decrement();
            health = enemyHealth.getHealth();
            StartCoroutine(hurtEffect());
            if(enemyHealth.getHealth() <= 0)
                return true;
        }
        return false;
    }

    void Die() {
        gameObject.SetActive(false);
    }

    IEnumerator hurtEffect(){
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(.5f);
        spriteRenderer.color = normalColor;
    }

    private void MortyRoll(Vector3 oldPos){
        Vector3 newPos = rb.position;
        float x = newPos.x - oldPos.x;

        float ratio = (x / circumference);
        float degrees = ratio * 360;
        // Debug.Log(x);
        transform.Rotate(Vector3.forward, -degrees);
    }
}
