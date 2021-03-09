using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSystem : MonoBehaviour
{
    //all of noodle rope stuff happens here
    /*
    1.  Distance joint 2d has two connected rigidbodies. This means that the end of the rope
        has to have an invisible rigidbody that extends?
    2.  When the right mouse button is pressed, the script raycasts along the line from the
        center of the player to the crosshair. When the raycast hits a layermask, the invisible
        rigidbody is positioned to the point of collision
    3.  A line renderer will draw a line between the two points
    4.  The player will need to move left and right. This might mean an adjustment to the player 
        controller to make the movement work. (don't know how the movement script will react)
    5.   W and S will make the rope shorter and longer.  
    */

    private DistanceJoint2D rope;
    private LineRenderer ropeVisual;
    // public Rigidbody2D anchor;
    public Transform body;
    private Vector2 mousePosition;
    private RaycastHit2D grapplePoint;
    public float rappelSpeed;
    public LayerMask grappleLayer;
    private ContactFilter2D contactFilter;
    private List<RaycastHit2D> hitPoints = new List<RaycastHit2D>(16);
    private int numberOfHitPoints;
    private bool isRopeOut;
    // Start is called before the first frame update
    void Start()
    {
        rope = GetComponent<DistanceJoint2D>();
        ropeVisual = GetComponent<LineRenderer>();
        // anchor = rope.connectedBody;
        ropeVisual.positionCount = 2;
        // grappleLayer = LayerMask.GetMask("grapple");
        isRopeOut = false;
        rope.enabled = false;
        contactFilter.SetLayerMask(LayerMask.GetMask("grapple"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            GetHitPos();
            // Debug.Log(isRopeOut);
        }
        if(isRopeOut){
            UpdateRope();
        }
    }

    private void GetHitPos() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)body.position;
        hitPoints.Clear();
        numberOfHitPoints = Physics2D.Raycast(body.position, direction, contactFilter, hitPoints);
        // Debug.Log(numberOfHitPoints);
        if(numberOfHitPoints > 0){
            isRopeOut = true;
            rope.enabled = true;
            rope.distance = (hitPoints[0].point - (Vector2)body.position).magnitude;
            rope.connectedBody.position = hitPoints[0].point;
        }
        else{
            isRopeOut = false;
            DestroyRope();
        }
    }

    private void UpdateRope(){
        if(!ropeVisual.enabled){
            ropeVisual.enabled = true;
        }
        // Debug.DrawLine(body.position, hitPoints[0].point);
        Vector3[] linePoints = {body.position, hitPoints[0].point};
        ropeVisual.SetPositions(linePoints);
    }

    private void DestroyRope(){
        ropeVisual.enabled = false;
        rope.enabled = false;
    }

    public bool GetIsRopeOut(){
        return isRopeOut;
    }
}
