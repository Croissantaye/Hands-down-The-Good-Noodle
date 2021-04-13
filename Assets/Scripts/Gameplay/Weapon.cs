using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected int ammo;
    [SerializeField] protected float speed;
    [SerializeField] protected int numProjectiles;
    protected int curAmmoCount;
    [SerializeField] protected float reloadTime;
    protected bool canShoot;

    protected virtual void fire()
    {
        Debug.Log("fire");
    }

    protected virtual void reload() 
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
