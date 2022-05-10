using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMovement : MonoBehaviour
{
    private Vector2 bulletDirection;
    [Range(.1f, 100f)] public float bulletSpeed = 10f;
    private Rigidbody2D rb;
    [Range(2f, 10f)] public float BulletLifetimeInSeconds;
    [Range(.01f, .2f)] public float OverlapCircleRadius;

    public void Setup(Vector2 direction, float speed){
        bulletDirection = direction;    
        bulletSpeed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(BulletLifetime());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 nextPosition = rb.position + (bulletDirection.normalized * bulletSpeed * Time.deltaTime);
        if(isValidPosition(nextPosition)){
            rb.position = nextPosition;
        }
        else{
            Destroy(gameObject);
        }
    }

    private bool isValidPosition(Vector3 nextPosition)
    {
        bool isValid = true;
        Collider2D collider = Physics2D.OverlapCircle(nextPosition, OverlapCircleRadius);
        if(collider != null && !GameObject.FindObjectOfType<GoodNoodleMovement>().gameObject.layer.Equals(collider.gameObject.layer)){
            isValid = false;
        }
        return isValid;
    }

    IEnumerator BulletLifetime(){
        yield return new WaitForSeconds(BulletLifetimeInSeconds);
        Destroy(gameObject);
    }
}
