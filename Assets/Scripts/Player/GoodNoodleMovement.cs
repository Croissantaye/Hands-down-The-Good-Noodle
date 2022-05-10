using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodNoodleMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 movementDirection;
    private bool isJumping;
    private float initalJumpForce;
    private float finalJumpForce;
    private float jumpStartY;

    //shown in editor
    [Range(.1f, 20f)]public float jumpMaxY = 2.2f;
    [Range(100f, 1000f)]public float movementForce = 500f;
    [Range(100f, 800f)]public float jumpForce = 10f;
    [Range(1f, 10f)]public float jumpForceDecrease = 3f;
    [Range(5f, 50f)]public float maxSpeed = 8f;

    [Header("Sounds")]
    public AudioClip shortJumpSound;
    public AudioClip longJumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementDirection = Vector3.zero;
        isJumping = false;
        initalJumpForce = jumpForce;
        finalJumpForce = initalJumpForce / jumpForceDecrease;
    }

    private void OnValidate() {
        initalJumpForce = jumpForce;
        finalJumpForce = initalJumpForce / jumpForceDecrease;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal")){
            movementDirection.x = Input.GetAxisRaw("Horizontal");
        }
        if(Input.GetButton("Jump")){
            if(!isJumping && GroundTest()){
                isJumping = true;
                jumpStartY = rb.position.y;

                GetComponent<PlayerSounds>().PlayByClip(shortJumpSound);
            }
            if(isJumping && rb.position.y > (jumpStartY + jumpMaxY)){
                isJumping = false;
            }
        }
        if(!Input.GetButton("Horizontal")){
            movementDirection.x = 0f;
        }
        if(Input.GetButtonUp("Jump")){
            isJumping = false;
        }
    }
    void FixedUpdate(){
        updateRB();
    }

    void updateRB(){
        if(Mathf.Abs(rb.velocity.x) < maxSpeed){
            rb.AddForce(new Vector2(movementDirection.x, 0f) * Time.deltaTime * movementForce);
        }
        if(isJumping){
            if(Input.GetButtonDown("Jump")){
                rb.AddForce(new Vector2(0f, initalJumpForce));
            }
            else{
                rb.AddForce(new Vector2(0f, finalJumpForce));
            }
        }
    }

    bool GroundTest(){
        bool isOnGround = false;
        Vector2 startPos = rb.position;
        startPos.y = rb.position.y - (GetComponent<CapsuleCollider2D>().size.y / 2f);
        startPos.x += .01f;
        RaycastHit2D result = Physics2D.Raycast(startPos, Vector2.down, .15f);
        if(result.collider != null){
            isOnGround = true;
        }
        return isOnGround;    
    }
}
