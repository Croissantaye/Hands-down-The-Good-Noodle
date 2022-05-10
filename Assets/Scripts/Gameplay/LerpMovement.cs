using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    private Vector2 leftPoint;
    private Vector2 rightPoint;
    private Vector2 offset;
    private float totalLength;
    private float initialMagnitude;
    private float initalT;
    public float modFactor;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        offset = new Vector2(2f, 0f);
        leftPoint = rb.position - offset;
        rightPoint = rb.position + offset;

        totalLength = (leftPoint - rightPoint).magnitude;
        initialMagnitude = (leftPoint - rb.position).magnitude;
        initalT = initialMagnitude/totalLength;
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = Vector2.Lerp(leftPoint, rightPoint, (Time.time % modFactor) / modFactor);
    }
}
