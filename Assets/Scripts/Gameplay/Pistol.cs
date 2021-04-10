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
            Projectile temp = Instantiate(bullet, aim.getGunPoint().position, Quaternion.identity);
            Vector3 direction = aim.getAimDirection();
            temp.Setup(direction, speed);
            temp = null;
            curAmmoCount--;
            Debug.Log("PISTOL ammo left: " + curAmmoCount);
        }
    }

    protected override void reload()
    {
        // Debug.Log("reload pistol");
        StartCoroutine(ReloadPistol());
    }

    private IEnumerator ReloadPistol(){
        yield return new WaitForSeconds(reloadTime);
        curAmmoCount = ammo;
        canShoot = true;
        // Debug.Log("Done reloading");
    }

}

