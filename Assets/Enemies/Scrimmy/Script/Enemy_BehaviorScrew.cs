using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BehaviorScrew : MonoBehaviour
{ 
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    float patrolDist;
    [SerializeField]
    float height;

    Vector2 StartPosition;
    Vector2 AbovePos;

    public bool MoveRight;
    public bool MoveUp;
    Rigidbody2D rb;
    Vector2 pr, pl;

    // Start is called before the first frame update
    void Start()
    {
        //Sets rigidbody
        rb = GetComponent<Rigidbody2D>();
        StartPosition = transform.position;
        pr = new Vector2(transform.position.x + patrolDist, transform.position.y);
        pl = new Vector2(transform.position.x - patrolDist, transform.position.y);
        MoveUp = true;
        MoveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        screw();
    }


    void screw()
    {
        //To the right of character start
        if (MoveRight)
        {
            Debug.Log("r");
            //Go Up
            if (transform.position.x >= pr.x && MoveUp)
            {
                Debug.Log("r up");
                if (transform.position.y >= pr.y)
                {
                    if (transform.position.y >= StartPosition.y + height)
                    {
                        MoveUp = false;
                    }
                    else { transform.Translate(0, 2 * Time.deltaTime * jumpSpeed, 0); }
                }
            }
            //Go Down
            else if(transform.position.x == pr.x && !MoveUp)
            {
                Debug.Log("r Down");
                if (transform.position.y <= StartPosition.y + height)
                {
                    transform.Translate(0, -2 * Time.deltaTime * jumpSpeed, 0);
                }
                //if touch ground, set goup false move left one space, set goright false
                else if(transform.position.x == StartPosition.x)
                {
                    transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
                    MoveUp = true;
                    MoveRight = false;
                }
            }
            //Go Right
            else
            {
                Debug.Log("r right");
                transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);
            }
        }

        //To the left of character start
        else
        {
            Debug.Log("l");
            //Go Up
            if (transform.position.x <= pr.x && MoveUp)
            {
                Debug.Log("l up");
                if (transform.position.y <= pr.y)
                {
                    if (transform.position.y >= StartPosition.y + height)
                    {
                        MoveUp = false;
                    }
                    else { transform.Translate(0, 2 * Time.deltaTime * jumpSpeed, 0); }
                }
            }
            //Go Down
            else if (transform.position.x == pr.x && !MoveUp)
            {
                Debug.Log("l down");
                if (transform.position.y <= StartPosition.y + height)
                {
                    transform.Translate(0, -2 * Time.deltaTime * jumpSpeed, 0);
                }
                //if touch ground, set goup false move left one space, set goright false
                else if (transform.position.x == StartPosition.x)
                {
                    transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
                    MoveUp = true;
                    MoveRight = true;
                }
            }
            //Go Left
            else
            {
                Debug.Log("l left");
                transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
            }
        }
    }
}
