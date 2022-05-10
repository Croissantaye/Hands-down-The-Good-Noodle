using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Vector3 rightFacingScale;
    private Vector3 leftFacingScale;
    private float buffer;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        capsuleCollider = transform.parent.GetComponent<CapsuleCollider2D>();

        rightFacingScale = new Vector3(1f, 1f, 1f);
        leftFacingScale = new Vector3(-1f, 1f, 1f);

        buffer = .05f;
    }

    // Update is called once per frame
    void Update()
    {
        // FlipOnMovement();

        FlipOnMousePosition();
    }

    private void FlipOnMovement(){
        if(rb.velocity.x > buffer && transform.localScale.x == -1f){
            transform.localScale = rightFacingScale;
            transform.GetChild(0).transform.localScale *= -1;
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x * -1, capsuleCollider.offset.y);
        }
        if(rb.velocity.x < -buffer && transform.localScale.x == 1f){
            transform.localScale = leftFacingScale;
            transform.GetChild(0).transform.localScale *= -1;
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x * -1, capsuleCollider.offset.y);
        }
    }

    private void FlipOnMousePosition(){
        //get mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //get vector from player to mousePos
        Vector2 mouseDirection = mousePos - rb.position;
        // if mouse is on the right of the player
        if(mouseDirection.x > 0 && transform.localScale.x == -1f){
            transform.localScale = rightFacingScale;
            transform.GetChild(0).transform.localScale *= -1;
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x * -1, capsuleCollider.offset.y);
        }
        if(mouseDirection.x < 0 && transform.localScale.x == 1f){
            transform.localScale = leftFacingScale;
            transform.GetChild(0).transform.localScale *= -1;
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x * -1, capsuleCollider.offset.y);
        }
    }
}