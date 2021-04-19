using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy moves left and right and turns when hits a wall
// jumps towards the player when the enemy sees the player
public class Enemy_TonyRigatoni : BasicEnemy
{
    private Animator animator;
    private Vector3 direction;
    public Transform Feet;
    public Transform Wall;
    private LayerMask wallMask;
    private ContactFilter2D playerFilter;
    private Vector3 startPosition;
    private Quaternion startRotation;
    [SerializeField] [Range (.1f, 20f)] float sightRange;
    [SerializeField] float jumpForce;

    private void OnEnable() {
        PlayerPlatformerController.Killed += reset;
    }
    private void OnDisable() {
        PlayerPlatformerController.Killed -= reset;
    }
    protected override void Start()
    {
        base.Start();
        animator = gameObject.GetComponent<Animator>();
        direction = Vector3.zero;
        wallMask = LayerMask.GetMask("ground");
        canDie =  false;
        playerFilter.SetLayerMask(LayerMask.GetMask("player"));
        playerFilter.useLayerMask = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update() {
        if(CheckForWall()){
            flipEnemy();
        }
    }

    private void FixedUpdate() {
        base.move(direction);
        lookForPlayer();
    }

    private bool CheckFeet(){
        bool IsGrounded;
        IsGrounded = Physics2D.OverlapCircle(Feet.transform.position, .1f, wallMask);
        return IsGrounded;
    }

    private bool CheckForWall(){
        bool hitWall;
        bool hitPlayer;
        hitWall = Physics2D.OverlapCircle(Wall.transform.position, .1f, wallMask);
        hitPlayer = Physics2D.OverlapCircle(Wall.transform.position, .1f, LayerMask.GetMask("player"));
        return hitWall || hitPlayer;
    }
    
    public void SetDirection(Vector3 vec){
        direction = vec;
    }

    public void SetCanDie(bool d){
        canDie = d;
    }

    private bool lookForPlayer(){
        List<RaycastHit2D> results = new List<RaycastHit2D>(16);
        int hitPoints = Physics2D.Raycast(Wall.transform.position, new Vector2(direction.x, 0), playerFilter, results, sightRange);
        if(hitPoints > 0){
            // Debug.Log("Player seen");
            Debug.DrawLine(Wall.transform.position, results[0].point, Color.green, 1);
            // rb.velocity = new Vector2(direction.x * speed * Time.fixedDeltaTime, jumpForce);
            if(CheckFeet())
                jumpToPlayer(results[0].point);
            results.Clear();
        }
        return true;
    }
    private void jumpToPlayer(Vector2 playerPos){
        // determine how high to jump to get to the player

        // get the distance to the player
        Vector2 distanceToPlayer = new Vector2(playerPos.x - Wall.transform.position.x, playerPos.y - Wall.transform.position.y);
        // get the amount of time to move horizontally
        float airTime = Mathf.Abs(distanceToPlayer.x) / speed;
        // vi = (Dy - .5a(t)^2) / t
        float initialVerticalForce = (float) ( (playerPos.y - Feet.transform.position.y) - (.5 * Physics2D.gravity.y * Mathf.Pow(airTime, 2) ) ) / airTime;
        Debug.Log("Jump force: " + initialVerticalForce);
        float jumpBuffer = .1f;
        rb.velocity = new Vector2(direction.x * speed * Time.fixedDeltaTime, initialVerticalForce + jumpBuffer);
    }

    private void flipEnemy(){
        transform.RotateAround(transform.position, Vector3.up, 180);
        direction = direction * -1;
    }

    private void reset(int a){
        // reset health back to max
        enemyHealth.setHealth(maxHealth);
        // reset position and rotation back to startPos and startRotation
        transform.position = startPosition;
        transform.rotation = startRotation;
        direction = Vector3.zero;
    }
}
