using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected int maxAmmo;
    protected int ammoCount;
    protected Transform gunpoint;
    protected GameObject BulletPrefab;
    protected Sprite WeaponSprite;
    protected float BulletSpeed;

    public virtual void WeaponSetup(int max, GameObject prefab, Sprite sprite, float speed){
        maxAmmo = max;
        ammoCount = maxAmmo;
        BulletPrefab = prefab;
        WeaponSprite = sprite;
        BulletSpeed = speed;
    }

    public virtual void WeaponSetup(int max, GameObject prefab, Sprite sprite, float speed, float spread, int bullets){
        maxAmmo = max;
        ammoCount = maxAmmo;
        BulletPrefab = prefab;
        WeaponSprite = sprite;
        BulletSpeed = speed;
    }

    public void UpdateArmSprite(){
        GetComponent<SpriteRenderer>().sprite = WeaponSprite;
    }

    public virtual void PrintCurrentAmmoCounts(){
        Debug.Log(ammoCount);
    }
    protected virtual void Start() {
        gunpoint = transform.GetChild(0);
        GetComponent<SpriteRenderer>().sprite = WeaponSprite;
    }

    public abstract void Fire();
    
    public void Reload(){
        if(ammoCount < maxAmmo){
            ammoCount = maxAmmo;
        }
    }
}
