using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : WeaponBase
{
    public override void Fire()
    {
        if(_AmmoCount > 0){
            GameObject bullet = Instantiate(_BulletPrefab, _Gunpoint.position, transform.rotation);
            bullet.GetComponent<PlayerBulletMovement>().Setup(GetComponent<ArmPointToMouse>().transform.right, _BulletSpeed);
            _AmmoCount--;
        }
    }
}
