using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenadeMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [Range(2f, 8f)] public float bulletLifetimeInSec;
    private Vector2 initalDirection;
    private float launchSpeed;
    private int bounceCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initalDirection * launchSpeed);
        bounceCount = 0;
        StartCoroutine(BulletLifetime());
    }

    public void Setup(Vector2 direction, float speed){
        initalDirection = direction;
        launchSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity.sqrMagnitude > .5f){
            Quaternion bulletRotation = new Quaternion();
            Vector3 Velocity3D = new Vector3(rb.velocity.x, rb.velocity.y, 0f);
            Vector3 lookDirection = Vector3.Cross(Vector3.forward, Velocity3D);
            bulletRotation.SetLookRotation(Vector3.forward, lookDirection);
            transform.rotation = bulletRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag != "Player"){
            bounceCount++;

            Debug.DrawRay(other.contacts[0].point, other.contacts[0].normal, Color.green, 1f);
            Debug.DrawRay(other.contacts[0].point, (rb.position - other.contacts[0].point).normalized, Color.red, 1f);

            // get the angle between the incoming collision angle and the normal of the collision
            Vector2 incomingCollision = (rb.position - other.contacts[0].point);
            Vector2 collsionNormal = other.contacts[0].normal;
            float incidenceAngle = (Vector2.Dot(incomingCollision, collsionNormal)) / (incomingCollision.magnitude * collsionNormal.magnitude);
            incidenceAngle = Mathf.Acos(incidenceAngle) * Mathf.Rad2Deg;

            // create quaternion representing rotation
            Quaternion rotation = Quaternion.Euler(0f, 0f, incidenceAngle);

            // multiply the collisionNormal by rotation to get bounce angle
            Vector2 bounceVector = rotation * collsionNormal;
            float bouceSignificance = ((1 / Mathf.Pow(bounceCount, 1.25f)) * bounceVector).sqrMagnitude;
            if(bouceSignificance > .5f){
                rb.AddForce((1 / Mathf.Pow(bounceCount + 1f, .5f)) * launchSpeed * bounceVector);
            }
        }
    }

    IEnumerator BulletLifetime(){
        yield return new WaitForSeconds(bulletLifetimeInSec);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
