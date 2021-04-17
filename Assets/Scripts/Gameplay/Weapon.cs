using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected int ammo;
    [SerializeField] protected float speed;
    [SerializeField] protected int numProjectiles;
    protected int curAmmoCount;
    [SerializeField] protected float reloadTime;
    protected bool canShoot;

    public delegate void AmmoChange(int currentAmmo, int maxAmmo);
    public static event AmmoChange Fired;
    public static event AmmoChange Reloaded;
    public static event AmmoChange WeaponSwitch;

    protected virtual void fire(int a, int b)
    {
        Debug.Log("fire");
    }

    protected virtual void reload(int a, int b) 
    {
        Debug.Log("reload");
    }

    public int GetCurrentAmmo(){
        return curAmmoCount;
    }

    public int GetMaxAmmo(){
        return ammo;
    }

    protected void RaiseFired(int a, int b){
        if(Fired != null){
            Fired(a, b);
        }
    }

    protected void RaiseReloaded(int a, int b){
        if(Reloaded != null){
            Reloaded(a, b);
        }
    }

    protected void RaiseWeaponSwitch(int a, int b){
        if(WeaponSwitch != null){
            WeaponSwitch(a, b);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
