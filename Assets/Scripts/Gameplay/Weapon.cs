using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int ammo;
    protected float speed;
    protected int numProjectiles;

    protected void fire()
    {
        Debug.Log("fire");
    }

    protected void reload() 
    {
        Debug.Log("reload");
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
