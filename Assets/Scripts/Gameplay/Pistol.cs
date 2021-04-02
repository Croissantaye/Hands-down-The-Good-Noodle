using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : Weapon
{
    private PlayerAimWeapon aim;
    [SerializeField] private Projectile bullet;
    [SerializeField] private float pistolSpeed;
    [SerializeField] private int BulletNum;

    // Start is called before the first frame update
    void Start()
    {
        aim = gameObject.GetComponentInChildren<PlayerAimWeapon>();
        ammo = 12;
        speed = pistolSpeed;
        numProjectiles = BulletNum;
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
    protected override void fire()
    {
        // Debug.Log("fire pistol");

        Projectile temp = Instantiate(bullet, aim.getGunPoint().position, Quaternion.identity);
        Vector3 direction = aim.getAimDirection();
        temp.Setup(direction, speed);
        temp = null;
    }

    protected override void reload()
    {
        Debug.Log("reload");
    }

}

