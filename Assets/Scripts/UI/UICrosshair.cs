using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrosshair : MonoBehaviour
{
    public Slider AmmoSlider;

    public Image Top;
    public Image Mid;
    public Image Bottom;

    private void OnEnable() {
        Weapon.Fired += updateAmmo;
        Weapon.Reloaded += updateAmmo;
        PlayerPlatformerController.Hurt += updateHealthCounter;
        PlayerPlatformerController.Change += updateAmmo;
    }

    private void OnDisable() {
        Weapon.Fired -= updateAmmo;
        Weapon.Reloaded -= updateAmmo;
        PlayerPlatformerController.Hurt -= updateHealthCounter;
        PlayerPlatformerController.Change -= updateAmmo;
    }

    private void LateUpdate() {
        transform.position = GetMousePosition();
    }

    private Vector2 GetMousePosition(){
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(mousePosition);
        return mousePosition;
    }
    
    public void updateAmmo(int ammoCount, int maxAmmo){
        // Debug.Log("Current ammo: " + ammoCount);
        // Debug.Log("Max ammo: " + maxAmmo);
        float ammoRatio = (float) ammoCount / maxAmmo;
        AmmoSlider.value = ammoRatio;
    }

    public void updateHealthCounter(int curHealth){
        if(curHealth == 1){
            Top.enabled = false;
            Mid.enabled = false;
            Bottom.enabled = true;
        }
        else if(curHealth == 2){
            Top.enabled = false;
            Mid.enabled = true;
            Bottom.enabled = true;
        }
        else{
            Top.enabled = true;
            Mid.enabled = true;
            Bottom.enabled = true;
        }
    }
}
