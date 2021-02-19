using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D rbCharacter;
    public LayerMask layerMask;
    //private BoxCollider2D collider;
    //public Rigidbody2D rbFeet;
    Vector2 move = new Vector2(0, 0);
    //public Transform feet;
    public float speedModifier;
    public float gravity;

    public float jumpForce = 10f;

    // JumpState jumpState = JumpState.grounded;
    
    //Start is called before the first frame update
    void Start()
    {
        rbCharacter = GetComponent<Rigidbody2D>();
        //collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal") * speedModifier;
        if(Input.GetButtonDown("Jump") && rbCharacter.velocity.y == 0){
            Jump();
        }
    }

    void FixedUpdate() {
        Vector2 direction = new Vector2(move.x * speedModifier, rbCharacter.velocity.y);

        rbCharacter.velocity = direction;
    }

    void Jump(){
        Vector2 direction = new Vector2(rbCharacter.velocity.x, jumpForce);
        rbCharacter.velocity = direction;
    }

    // enum JumpState
    // {
    //     jumping,
    //     grounded
    // }
}
