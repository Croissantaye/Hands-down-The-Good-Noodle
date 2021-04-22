using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BehaviorScrimmy : BasicEnemy
{
    [Header("Numeric values")]
    [SerializeField] private int scrimmyHealth;
    [SerializeField] private float Pause;
    [SerializeField] [Range(.1f, 10f)] private float jumpHeight;
    [SerializeField] [Range(.1f, 3f)] private float HorizontalSpeed;
    [SerializeField] [Range(.1f, 10f)] private float jumpTime;
    [SerializeField] [Range(1, 10)] private int jumpNumber;
    [SerializeField] private bool IsLookingLeft;

    [Header("Check for ground")]
    [SerializeField] private Transform GroundCheck; 

    //private variables 
    private ContactFilter2D contactFilter;
    private Vector2 velocity;
    private float jumpForce;
    private int jumpCounter;
    private bool canJump;
    private bool charging;
    private Animator animator;

    enum movementState{
        InAir,
        OnGround
    }
    // private movementState currentMovementState;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<HealthSystem>();
        animator = GetComponent<Animator>();
        enemyHealth.setAll(scrimmyHealth);

        // currentMovementState = movementState.OnGround;

        jumpForce = (transform.position.y + jumpHeight) - transform.position.y + (.5f * Physics2D.gravity.magnitude);

        velocity = new Vector2(HorizontalSpeed, jumpForce);
        canJump = false;
        jumpCounter = 0;
        charging = false;

        normalColor = spriteRenderer.color;
        
        HorizontalSpeed *= -1;
        if(!IsLookingLeft){
            flipDirection();
        }

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask.GetMask("foreground"));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update(){
        if(CheckGround() && !charging){
            StartCoroutine(PauseOnGround());
        }
        if(jumpCounter >= jumpNumber){
            jumpCounter = 0;
            flipDirection();
        }
        // Debug.Log(velocity);
    }

    private void FixedUpdate() {
        if(canJump){
            move(velocity);
            // currentMovementState = movementState.InAir;
            canJump = false;
            jumpCounter++;
        }
    }
    protected override void move(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
        velocity = Vector2.zero;
    }

    IEnumerator PauseOnGround(){
        // Debug.Log(transform.position);
        charging = true;
        animator.SetBool("charging", true);
        yield return new WaitForSeconds(Pause);
        canJump = true;
        velocity = new Vector2(HorizontalSpeed, jumpForce);
        animator.SetBool("charging", false);
        // buffer so the coroutine doesn't start right away
        yield return new WaitForSeconds(.1f);
        charging = false;
    }

    // should only be called in FixedUpdate because of physics
    private bool CheckGround()
    {
        bool hitGround;
        hitGround = Physics2D.OverlapCircle(GroundCheck.position, .1f, LayerMask.GetMask("ground"));
        return hitGround;
    }

    protected void flipDirection()
    {
        // flip  from left to right
        if(!spriteRenderer.flipX){
            spriteRenderer.flipX = true;
            HorizontalSpeed *= -1;
        }
        // flip from right to left
        else{
            spriteRenderer.flipX = false;
            HorizontalSpeed *= -1;
        }
    }
 enum FaceDirection
    {
        Left,
        Right
    }
}

