using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : Weapon
{
    [SerializeField] [Range (.1f, 30f)] private int SpreadAngle;
    private PlayerAimWeapon aim;
    [SerializeField] private Projectile bullet;
    // [SerializeField] private float shotgunSpeed;
    // [SerializeField] private int BulletNum;

    // Start is called before the first frame update
    void Start()
    {
        aim = gameObject.GetComponentInChildren<PlayerAimWeapon>();
        canShoot = true;
        curAmmoCount = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if(curAmmoCount <= 0){
            canShoot = false;
        }
    }

    private void OnEnable()
    {
        PlayerPlatformerController.Shoot += fire;
        PlayerPlatformerController.Reload += reload;
    }

    private void OnDisable()
    {
        PlayerPlatformerController.Shoot -= fire;
        PlayerPlatformerController.Reload -= reload;
    }



    ///Inherited from Weapon/Parent
    protected override void fire()
    {
        if(canShoot){
            // Debug.Log("fire shotgun");
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
            curAmmoCount -= numProjectiles;
            Debug.Log("SHOTGUN ammo left: " + curAmmoCount);
        }
    }

    protected override void reload()
    {
        // Debug.Log("reload pistol");
        StartCoroutine(ReloadShotgun());
    }

    private IEnumerator ReloadShotgun(){
        yield return new WaitForSeconds(reloadTime);
        curAmmoCount = ammo;
        canShoot = true;
        // Debug.Log("Done reloading");
    }

}
