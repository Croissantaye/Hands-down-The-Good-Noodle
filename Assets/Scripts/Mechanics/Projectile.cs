using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;

    public void Setup(Vector3 dir){
        this.direction = dir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(direction));
        Destroy(gameObject, 5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected float GetAngleFromVectorFloat(Vector3 dir){
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n < 0)
            n += 360;

        return n;
    }
}
