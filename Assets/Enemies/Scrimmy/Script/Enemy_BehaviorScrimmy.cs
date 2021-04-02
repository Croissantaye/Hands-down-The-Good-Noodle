using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BehaviorScrimmy : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartPos = transform.position;
        LeftLimit = new Vector2(StartPos.x - PatrolLimit, StartPos.y);
        RightLimit = new Vector2(StartPos.x + PatrolLimit, StartPos.y);
        UpLimit = new Vector2(StartPos.x, StartPos.y + JumpHeight);
        dir = Vector2.zero;
        MoveUp = true;
        MoveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 oldPosition = rb2d.position;

        //If moving to the right
        if (MoveRight)
        {
            Debug.Log("Scrimmy R");
            dir = Vector2.right;
            //If scrimmy has made it to the patrol limit to the right
            if (rb2d.position.x >= RightLimit.x)
            {
                //If Scrimmy must move up
                if (MoveUp)
                {
                    if (rb2d.position.y >= UpLimit.y)
                    {
                        MoveUp = false;
                        dir = Vector2.down;
                    }
                    else
                    {
                        Debug.Log("Scrimmy RU");
                        rb2d.position = new Vector2(RightLimit.x, rb2d.position.y);
                        dir = Vector2.up;
                    }
                }
            }
            else
            {
                if (rb2d.position.y > StartPos.y)
                {
                    dir = Vector2.down;
                }
                else
                {
                    Debug.Log("Scrimmy RDL");
                    dir = Vector2.left;
                    MoveUp = true;
                    MoveRight = false;
                    StartCoroutine(PauseOnGround());
                }
               
            }
        }
        if (!MoveRight)
        {
            Debug.Log("Scrimmy L");
            dir = Vector2.left;
            if(rb2d.position.x <= LeftLimit.x)
            {
                if (MoveUp)
                {
                    if (rb2d.position.y >= UpLimit.y)
                    {
                        MoveUp = false;
                        dir = Vector2.down;
                    }
                    else
                    {
                        Debug.Log("Scrimmy LU");
                        rb2d.position = new Vector2(LeftLimit.x, rb2d.position.y);
                        dir = Vector2.up;
                    }
                }
                else
                {
                    if(rb2d.position.y > StartPos.y)
                    {
                        dir = Vector2.down;
                    }
                    else
                    {
                        Debug.Log("Scrimmy DLR");
                        dir = Vector2.right;
                        MoveUp = true;
                        MoveRight = true;
                        StartCoroutine(PauseOnGround());
                    }

                }
            }
        }

        move(dir);
    }

    void move(Vector2 DIR)
    {
        rb2d.position = rb2d.position + (DIR * MoveSpeed * Time.deltaTime);
    }


    IEnumerator PauseOnGround(){
        yield return new WaitForSeconds(Pause);
    }
}

