using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : Weapon
{
    private PlayerAimWeapon aim;
    [SerializeField] private Projectile bullet;

    // Start is called before the first frame update
    void Start()
    {
        aim = gameObject.GetComponentInChildren<PlayerAimWeapon>();
        curAmmoCount = ammo;
        canShoot = true;
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
        // PlayerPlatformerController.Shoot(curAmmoCount, maxAmmo) += fire;
        PlayerPlatformerController.Shoot += fire;
        PlayerPlatformerController.Reload += reload;
    }

    private void OnDisable()
    {
        PlayerPlatformerController.Shoot -= fire;
        PlayerPlatformerController.Reload -= reload;
    }



    ///Inherited from Weapon/Parent
    protected override void fire(int curAmmo, int maxAmmo)
    {
        if(canShoot){
            Projectile temp = Instantiate(bullet, aim.getGunPoint().position, Quaternion.identity);
            Vector3 direction = aim.getAimDirection();
            temp.Setup(direction, speed);
            temp = null;
            curAmmoCount--;
            RaiseFired(GetCurrentAmmo(), GetMaxAmmo());
            // Debug.Log("PISTOL ammo left: " + curAmmoCount);
        }
    }

    protected override void reload(int a, int b)
    {
        // Debug.Log("reload pistol");
        StartCoroutine(ReloadPistol());
    }

    private IEnumerator ReloadPistol(){
        // Debug.Log("Current ammo left: " + curAmmoCount);
        yield return new WaitForSeconds(reloadTime);
        curAmmoCount = ammo;
        RaiseReloaded(GetCurrentAmmo(), GetMaxAmmo());
        canShoot = true;
        // Debug.Log("Done reloading");
    }

}

