using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrenadeLauncher : WeaponBase
{
    public override void Fire()
    {
        if(_AmmoCount > 0){
            GameObject bullet = Instantiate(_BulletPrefab, _Gunpoint.position, transform.rotation);
            bullet.GetComponent<PlayerGrenadeMovement>().Setup(GetComponent<ArmPointToMouse>().transform.right, _BulletSpeed);
            _AmmoCount--;
        }
    }
}
