using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collision;
    private Vector2 startPos;
    public float speed;

    public int unitsMoved;

    private bool faceLeft;

    private float leftLimit;
    private float rightLimit;

    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       collision = GetComponent<BoxCollider2D>();

       startPos = rb.position;

       faceLeft = true;

        leftLimit = startPos.x - unitsMoved;
        rightLimit = startPos.x + unitsMoved;

        direction = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (faceLeft)
       {
           direction = Vector2.left;
           if(rb.position.x <= leftLimit)
           {
               rb.position = new Vector2(leftLimit, rb.position.y);
               direction = Vector2.right;
               faceLeft = false;
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
           }
       }

       //print(faceLeft);
       move(direction);
    }

    void move(Vector2 direction)
    {
       rb.position = rb.position + (direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collider) 
    {
        if(shouldDieFromCollision(collider))
        {
            Die();
        }
    }

    private bool shouldDieFromCollision(Collision2D collision)
    {
        PlayerPlatformerController player = collision.gameObject.GetComponent<PlayerPlatformerController>();
        if(player && collision.GetContact(0).normal.y <= -.5f)
        {
            return true;
        }
        return false;
    }

    void Die() {
        gameObject.SetActive(false);
    }
}
