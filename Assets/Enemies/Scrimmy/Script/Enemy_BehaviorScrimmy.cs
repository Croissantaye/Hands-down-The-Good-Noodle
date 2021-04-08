using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BehaviorScrimmy : BasicEnemy
{
    [SerializeField]
    float MoveSpeed;
    [SerializeField]
    float JumpHeight;
    [SerializeField]
    float PatrolLimit;
    [SerializeField]
    float Pause;

    private Rigidbody2D rb2d;
    // private bool MoveRight;
    // private bool MoveUp;
    private Vector2 StartPos;
    private Vector2 dir;
    private Vector2 LeftLimit;
    private Vector2 RightLimit;
    private Vector2 UpLimit;
    
    private bool ChargingJump;
    private FaceDirection LookDirection;
    [SerializeField]
    private bool IsLookingLeft;
    private ContactFilter2D contactFilter;

    enum movementState{
        Charging,
        Jumping,
        Falling,
        Landing
    }
    private movementState currentMovementState;
    //private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartPos = transform.position;
        LeftLimit = new Vector2(StartPos.x - PatrolLimit, StartPos.y);
        RightLimit = new Vector2(StartPos.x + PatrolLimit, StartPos.y);
        UpLimit = new Vector2(StartPos.x, StartPos.y + JumpHeight);
        dir = Vector2.zero;
        // MoveUp = true;
        // MoveRight = true;
        ChargingJump = true;
        currentMovementState = movementState.Charging;
        if (!IsLookingLeft)
        {
            LookDirection = FaceDirection.Right;
            flipDirection();
        }
        else
        {
            LookDirection = FaceDirection.Left;
        }
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask.GetMask("foreground"));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update(){
        if(currentMovementState == movementState.Charging){
            StartCoroutine(PauseOnGround());
        }
        else if(currentMovementState == movementState.Jumping){
            if(transform.position.y < UpLimit.y){
                dir.x = .25f;
                dir.y = 10f;
            }
            else{
                dir.y = 0f;
                currentMovementState = movementState.Falling;
            }
        }
        else if(currentMovementState == movementState.Falling){
            if(CheckGround()){
                currentMovementState = movementState.Landing;
            }
        }
        else{
            currentMovementState = movementState.Charging;
        }
    }

    private void FixedUpdate() {
        if(currentMovementState != movementState.Charging)
            move(dir);
    }

    protected override void move(Vector2 DIR)
    {
        rb2d.position = rb2d.position + (DIR * MoveSpeed * Time.deltaTime);
        // Debug.Log(dir);
    }


    IEnumerator PauseOnGround(){
        Debug.Log("PauseOnGround");
        yield return new WaitForSeconds(Pause);
        currentMovementState = movementState.Jumping;
    }

    private bool CheckGround()
    {
        bool onGround;
        
        // Debug.Log(GetDirection());
        List<RaycastHit2D> results = new List<RaycastHit2D>(16);
        // Debug.DrawRay(transform.position, GetDirection(), Color.blue, 1f);
       // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .01f);
        int hitnum = Physics2D.Raycast(transform.position, Vector2.down, contactFilter, results, .5f);

        if (hitnum > 0)
        {
            onGround = true;
            Debug.Log("onGround");
        }
        else
        {
            onGround = false;
        }
        return onGround;
    }


    protected void flipDirection()
    {
        if (spriteRenderer.flipX) {
            spriteRenderer.flipX = false;
            LookDirection = FaceDirection.Left;
        }
        else
        {
            spriteRenderer.flipX = true;
            LookDirection = FaceDirection.Right;
        }
    }
 enum FaceDirection
    {
        Left,
        Right
    }
}

