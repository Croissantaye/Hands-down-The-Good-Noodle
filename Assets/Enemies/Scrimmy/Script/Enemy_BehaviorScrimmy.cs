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
    private bool MoveRight;
    private bool MoveUp;
    private Vector2 StartPos;
    private Vector2 dir;
    private Vector2 LeftLimit;
    private Vector2 RightLimit;
    private Vector2 UpLimit;
    
    
  

    private bool CanJump;
    private FaceDirection LookDirection;
    [SerializeField]
    private bool IsLookingLeft;
    private ContactFilter2D contactFilter;
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
        MoveUp = true;
        MoveRight = true;
        CanJump = false;
        StartCoroutine(PauseOnGround());
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
    void Update()
    {
        Vector2 oldPosition = rb2d.position;

        //If moving to the right
        //if (MoveRight)
        //{
        //    Debug.Log("Scrimmy R");
        //    dir = Vector2.right;
        //    //If scrimmy has made it to the patrol limit to the right
        //    if (rb2d.position.x >= RightLimit.x)
        //    {
        //        //If Scrimmy must move up
        //        if (MoveUp)
        //        {
        //            if (rb2d.position.y >= UpLimit.y)
        //            {
        //                MoveUp = false;
        //                dir = Vector2.down;
        //            }
        //            else
        //            {
        //                Debug.Log("Scrimmy RU");
        //                rb2d.position = new Vector2(RightLimit.x, rb2d.position.y);
        //                dir = Vector2.up;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (rb2d.position.y > StartPos.y)
        //        {
        //            dir = Vector2.down;
        //        }
        //        else
        //        {
        //            Debug.Log("Scrimmy RDL");
        //            dir = Vector2.left;
        //            MoveUp = true;
        //            MoveRight = false;
        //            StartCoroutine(PauseOnGround());
        //        }

        //    }
        //}
        //if (!MoveRight)
        //{
        //    Debug.Log("Scrimmy L");
        //    dir = Vector2.left;
        //    if(rb2d.position.x <= LeftLimit.x)
        //    {
        //        if (MoveUp)
        //        {
        //            if (rb2d.position.y >= UpLimit.y)
        //            {
        //                MoveUp = false;
        //                dir = Vector2.down;
        //            }
        //            else
        //            {
        //                Debug.Log("Scrimmy LU");
        //                rb2d.position = new Vector2(LeftLimit.x, rb2d.position.y);
        //                dir = Vector2.up;
        //            }
        //        }
        //        else
        //        {
        //            if(rb2d.position.y > StartPos.y)
        //            {
        //                dir = Vector2.down;
        //            }
        //            else
        //            {
        //                Debug.Log("Scrimmy DLR");
        //                dir = Vector2.right;
        //                MoveUp = true;
        //                MoveRight = true;
        //                StartCoroutine(PauseOnGround());
        //            }

        //        }
        //    }
        //}
        Debug.Log(CanJump);
        Debug.Log(transform.position.y + ", UL:" + UpLimit.y);
       if(CanJump && transform.position.y < UpLimit.y)
            {
            dir.y = 10f;
            
            }
            else
            {

                CanJump = false;
                dir.y = 0f;
            }
        //In The Air
        if(!CheckGround()){
            if (LookDirection == FaceDirection.Left)
            {
                dir.x = -.25f;
            }
            else
            {
                dir.x = .25f;
            }
           
        }
        //On The Ground
        else
        {
            StartCoroutine(PauseOnGround());

        }

        move(dir);
    }

    protected override void move(Vector2 DIR)
    {
        rb2d.position = rb2d.position + (DIR * MoveSpeed * Time.deltaTime);
        Debug.Log(dir);
    }


    IEnumerator PauseOnGround(){
        Debug.Log("PauseOnGround");
        yield return new WaitForSeconds(Pause);
        CanJump = true;
        
    }

    private bool CheckGround()
    {
        bool onGround;
        
        // Debug.Log(GetDirection());
        List<RaycastHit2D> results = new List<RaycastHit2D>(16);
        // Debug.DrawRay(transform.position, GetDirection(), Color.blue, 1f);
       // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .01f);
        int hitnum = Physics2D.Raycast(transform.position, Vector2.down, contactFilter, results, .1f);

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

