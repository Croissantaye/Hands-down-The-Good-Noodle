using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 direction;
    protected float speed;
    protected Rigidbody2D rb2d;

    public void Setup(Vector3 dir, float s){
        speed = s;
        this.direction = dir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(direction));
        Destroy(gameObject, 5f);
    }
    // Start is called before the first frame update
    protected void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        Vector3 oldPos = rb2d.position;
        Vector3 newPos = oldPos + (direction.normalized * speed * Time.fixedDeltaTime);
        rb2d.position = newPos;
    }

    protected float GetAngleFromVectorFloat(Vector3 dir){
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n < 0)
            n += 360;

        return n;
    }
    public void hit(){
        Destroy(gameObject);
    }

    //little bit janky, don't really like relying on a string to check
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Tilemap_LevelMap"){
            hit();
        }
    }

    public virtual bool IsEnemyProjectile(){
        return false;
    }
}
