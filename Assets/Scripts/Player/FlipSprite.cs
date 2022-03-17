using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 rightFacingScale;
    private Vector3 leftFacingScale;
    private float buffer;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();

        rightFacingScale = new Vector3(1f, 1f, 1f);
        leftFacingScale = new Vector3(-1f, 1f, 1f);

        buffer = .05f;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x > buffer && transform.localScale.x == -1f){
            transform.localScale = rightFacingScale;
        }
        if(rb.velocity.x < -buffer && transform.localScale.x == 1f){
            transform.localScale = leftFacingScale;
        }
        Debug.Log(Mathf.Sign(rb.velocity.x));
    }
}