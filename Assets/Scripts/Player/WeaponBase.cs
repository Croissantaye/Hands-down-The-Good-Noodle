using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected int _MaxAmmo;
    public int MaxAmmo => _MaxAmmo;
    protected int _AmmoCount;
    public int AmmoCount => _AmmoCount;
    protected Transform _Gunpoint;
    protected GameObject _BulletPrefab;
    protected Sprite _WeaponSprite;
    public Sprite WeaponSprite => _WeaponSprite;
    protected float _BulletSpeed;

    public virtual void WeaponSetup(int max, GameObject prefab, Sprite sprite, float speed){
        _MaxAmmo = max;
        _AmmoCount = _MaxAmmo;
        _BulletPrefab = prefab;
        _WeaponSprite = sprite;
        _BulletSpeed = speed;
    }

    public virtual void WeaponSetup(int max, GameObject prefab, Sprite sprite, float speed, float spread, int bullets){
        _MaxAmmo = max;
        _AmmoCount = _MaxAmmo;
        _BulletPrefab = prefab;
        _WeaponSprite = sprite;
        _BulletSpeed = speed;
    }

    public void UpdateArmSprite(){
        GetComponent<SpriteRenderer>().sprite = _WeaponSprite;
    }

    public virtual void PrintCurrentAmmoCounts(){
        Debug.Log(_AmmoCount);
    }
    protected virtual void Start() {
        _Gunpoint = transform.GetChild(0);
        GetComponent<SpriteRenderer>().sprite = _WeaponSprite;
    }

    public abstract void Fire();
    
    public void Reload(){
        if(_AmmoCount < _MaxAmmo){
            _AmmoCount = _MaxAmmo;
        }
    }
}
