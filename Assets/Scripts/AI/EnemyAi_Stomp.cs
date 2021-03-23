using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi_Stomp : MonoBehaviour
{
    [SerializeField]
    Transform player;

    float agroRange;
    float distanceToFloor;

    [SerializeField]
    float moveSpeed;

    Vector2 OrigPosition;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        //Sets rigidbody
        rb2d = GetComponent<Rigidbody2D>();

        //Gets original position to return to after stomp
        OrigPosition = transform.position;
        //Gets height of object using RayCast
        distanceToFloor = OrigPosition[1];
        //agrorange is now just slightly to the sides of the stomping enemy
        agroRange = distanceToFloor + 1;
   


    }

    // Update is called once per frame
    void Update()
    {
        //Checks Distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        

        if(distToPlayer < agroRange)
        {
            //Stomp on Player
            stomp();
        }
        else
        {
            unstomp();
        }
        

    }

    void stomp() {
        //if the player is withing 1 unit to the left and right of the enemy
        if (transform.position.y > player.position.y)
        {
            if (transform.position.y >= OrigPosition.y)
            {
                if (transform.position.x < player.position.x + 1)
                {
                    if (transform.position.x > player.position.x - 1)
                    {

                        //stomp code here
                        rb2d.velocity = new Vector2(0, -moveSpeed);
                    }
                }
            }
        }
    }

    void unstomp() {
        if(transform.position.y < OrigPosition.y)
        {
            rb2d.velocity = new Vector2(0, 1);
        }
        else { rb2d.velocity = new Vector2(0, 0); };
    }
}
