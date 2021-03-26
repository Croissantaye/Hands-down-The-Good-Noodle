using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 10f;
    private Rigidbody2D rb2d;

    public void Setup(Vector3 dir, float s){
        speed = s;
        this.direction = dir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(direction));
        Destroy(gameObject, 5f);
    }
    // Start is called before the first frame update
    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
}
