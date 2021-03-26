using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : Weapon
{
    [SerializeField] private int SpreadAngle = 15;
    private PlayerAimWeapon aim;
    [SerializeField] private Projectile bullet;
    [SerializeField] private float shotgunSpeed;
    [SerializeField] private int BulletNum;

    // Start is called before the first frame update
    void Start()
    {
        aim = gameObject.GetComponentInChildren<PlayerAimWeapon>();
        ammo = 6;
        speed = shotgunSpeed;
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
    private void fire()
    {
        Debug.Log("fire shotgun");
        int numOfSections = numProjectiles - 1;
        float angleOffset = aim.getAimAngle() + (SpreadAngle / 2);

        for(int i = 0; i < numProjectiles; i++)
        {
            // determine the angle offset for the bullet
            float bulletAngle = angleOffset - ((SpreadAngle / numOfSections) * i);
            bulletAngle = Mathf.Deg2Rad * bulletAngle;
            // setup each projectile to go on a different angle
            Projectile temp = Instantiate(bullet, aim.getGunPoint().position, Quaternion.identity);
            Vector3 direction = new Vector3(Mathf.Cos(bulletAngle), Mathf.Sin(bulletAngle), 0f);
            temp.Setup(direction, speed);
            temp = null;
        }
    }

    private void reload()
    {
        Debug.Log("reload");
    }

}
