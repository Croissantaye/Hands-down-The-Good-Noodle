using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodNoodleMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 movementDirection;

    //shown in editor
    [Range(100f, 1000f)]public float movementForce = 500f;
    [Range(.1f, 10f)]public float jumpForce = 10f;
    [Range(5f, 50f)]public float maxSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal")){
            movementDirection.x = Input.GetAxisRaw("Horizontal");
        }
        if(Input.GetButtonDown("Jump")){
            if(GroundTest()){
                movementDirection.y = jumpForce;
            }
        }
        if(!Input.GetButton("Horizontal")){
            movementDirection.x = 0f;
        }
        if(!GroundTest()){
            movementDirection.y = 0f;
        }
    }
    void FixedUpdate(){
        updateRB();
    }

    void updateRB(){
        if(Mathf.Abs(rb.velocity.x) < maxSpeed){
            rb.AddForce(new Vector2(movementDirection.x, 0f) * Time.deltaTime * movementForce);
        }
        if(movementDirection.y != 0f){
            rb.AddForce(new Vector2(0f, movementDirection.y) * Time.deltaTime * movementForce);
        }
    }

    bool GroundTest(){
        bool isOnGround = false;
        Vector2 startPos = rb.position;
        startPos.y = rb.position.y - (GetComponent<CapsuleCollider2D>().size.y / 2f);
        RaycastHit2D result = Physics2D.Raycast(startPos,Vector2.down, .15f);
        if(result.collider != null){
            isOnGround = true;
        }
        return isOnGround;    
    }
}
