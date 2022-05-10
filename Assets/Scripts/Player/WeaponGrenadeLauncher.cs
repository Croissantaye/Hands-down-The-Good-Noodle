using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrenadeLauncher : WeaponBase
{
    public override void Fire()
    {
        if(ammoCount > 0){
            GameObject bullet = Instantiate(BulletPrefab, gunpoint.position, transform.rotation);
            bullet.GetComponent<PlayerGrenadeMovement>().Setup(GetComponent<ArmPointToMouse>().transform.right, BulletSpeed);
            ammoCount--;
        }
    }
}
