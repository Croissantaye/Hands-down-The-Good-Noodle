using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWeaponControls : MonoBehaviour
{
    public enum WeaponType
    {
        pistol,
        shotgun,
        grenadeLauncher
    }

    private UI_AmmoController AmmoUI;
    private WeaponBase CurrentWeapon;

    private WeaponPistol _PistolComponent;
    private WeaponShotgun _ShotgunComponent;
    private WeaponGrenadeLauncher _GrenadeLauncherComponent;
    public WeaponType CurrentWeaponType;

    [Header("Pistol Data")]
    public int PistolMaxAmmo;
    public GameObject PistolBulletPrefab;
    public Sprite PistolSprite;
    [Range(2f,15f)] public float PistolBulletSpeed;

    [Header("Shotgun Data")]
    public int ShotgunMaxAmmo;
    public GameObject ShotgunBulletPrefab;
    public Sprite ShotgunSprite;
    [Range(2f,15f)] public float ShotgunBulletSpeed;
    [Range(15f,50f)] public float SpreadAngle;
    [Range(1f,10f)] public int BulletsPerShot;

    [Header("Grenade Launcher Data")]
    public int GrenadeLauncherMaxAmmo;
    public GameObject GrenadeLauncherBulletPrefab;
    public Sprite GrenadeLauncherSprite;
    [Range(100f,500f)] public float GrenadeLauncherBulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _PistolComponent = GetComponent<WeaponPistol>();
        _PistolComponent.WeaponSetup(PistolMaxAmmo, PistolBulletPrefab, PistolSprite, PistolBulletSpeed);
        _ShotgunComponent = GetComponent<WeaponShotgun>();
        _ShotgunComponent.WeaponSetup(ShotgunMaxAmmo, ShotgunBulletPrefab, ShotgunSprite, ShotgunBulletSpeed, SpreadAngle, BulletsPerShot);
        _GrenadeLauncherComponent = GetComponent<WeaponGrenadeLauncher>();
        _GrenadeLauncherComponent.WeaponSetup(GrenadeLauncherMaxAmmo, GrenadeLauncherBulletPrefab,GrenadeLauncherSprite,GrenadeLauncherBulletSpeed);

        AmmoUI = Camera.main.gameObject.GetComponentInChildren<UI_AmmoController>();

        CurrentWeapon = GetComponent<WeaponPistol>();
        SwitchWeapon(CurrentWeaponType);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){            
            CurrentWeapon.Fire();
            AmmoUI.UpdateAmmoUI(CurrentWeapon.AmmoCount);
        }
        if(Input.GetButtonDown("Reload")){
            Debug.Log("reload");
            CurrentWeapon.Reload();
            AmmoUI.UpdateAmmoUI(CurrentWeapon.AmmoCount, CurrentWeapon.MaxAmmo);
        }
        if(Input.GetButtonDown("Num1")){
            SwitchWeapon(WeaponType.pistol);
        }
        if(Input.GetButtonDown("Num2")){
            SwitchWeapon(WeaponType.shotgun);
        }
        if(Input.GetButtonDown("Num3")){
            SwitchWeapon(WeaponType.grenadeLauncher);
        }
        if(Input.GetButtonDown("Print")){
            CurrentWeapon.PrintCurrentAmmoCounts();
        }
    }

    private void SwitchWeapon(WeaponType newWeaponType){
        CurrentWeaponType = newWeaponType;

        if(CurrentWeapon.enabled){
            CurrentWeapon.enabled = false;
        }
        switch (CurrentWeaponType)
        {
            case WeaponType.pistol:
                CurrentWeapon = GetComponent<WeaponPistol>();
                CurrentWeapon.UpdateArmSprite();
                break;
            case WeaponType.shotgun:
                CurrentWeapon = GetComponent<WeaponShotgun>();
                CurrentWeapon.UpdateArmSprite();
                break;
            case WeaponType.grenadeLauncher:
                CurrentWeapon = GetComponent<WeaponGrenadeLauncher>();
                CurrentWeapon.UpdateArmSprite();
                break;
            default:
                break;
        }
        CurrentWeapon.enabled = true;
        AmmoUI.UpdateAmmoUI(CurrentWeapon.WeaponSprite, CurrentWeapon.AmmoCount, CurrentWeapon.MaxAmmo);
    }
}