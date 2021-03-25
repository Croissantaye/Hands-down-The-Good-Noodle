using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : Weapon
{
    [SerializeField] private int SpreadAngle = 15;
    private PlayerAimWeapon aim;
    [SerializeField] private Projectile bullet;

    // Start is called before the first frame update
    void Start()
    {
        aim = gameObject.GetComponentInChildren<PlayerAimWeapon>();
        ammo = 6;
        speed = 10f;
        numProjectiles = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerPlatformerController.Shoot += fire;
    }

    private void OnDisable()
    {
        PlayerPlatformerController.Shoot -= fire;
    }



    ///Inherited from Weapon/Parent
    private void fire()
    {
        Debug.Log("fire shotgun");
        
        for(int i = 0; i < numProjectiles; i++)
        {

        }
    }

    private void reload()
    {
        Debug.Log("reload");
    }

}
