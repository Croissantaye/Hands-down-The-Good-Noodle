using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : WeaponBase
{
    private float SpreadAngle;
    private int BulletsPerShot;
    public override void WeaponSetup(int max, GameObject prefab, Sprite sprite, float speed, float spread, int bullets){
        base.WeaponSetup(max, prefab, sprite, speed);
        SpreadAngle = spread;
        BulletsPerShot = bullets;
    }
    public override void Fire(){
        if(ammoCount > 0){
            float bulletAngle = transform.GetChild(0).eulerAngles.z  + (SpreadAngle / 2);
            for(int i = 0; i < BulletsPerShot; i++){
                bulletAngle *= Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(bulletAngle), Mathf.Sin(bulletAngle), 0f);
                GameObject bullet = Instantiate(BulletPrefab, gunpoint.position, transform.rotation);
                bullet.GetComponent<PlayerBulletMovement>().Setup(direction, BulletSpeed);
                bulletAngle *= Mathf.Rad2Deg;
                bulletAngle -= (SpreadAngle / BulletsPerShot);
            }
            ammoCount--;            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
