using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : WeaponBase
{
    public override void Fire()
    {
        if(ammoCount > 0){
            GameObject bullet = Instantiate(BulletPrefab, gunpoint.position, transform.rotation);
            bullet.GetComponent<PlayerBulletMovement>().Setup(GetComponent<ArmPointToMouse>().transform.right, BulletSpeed);
            ammoCount--;
        }
    }
}
