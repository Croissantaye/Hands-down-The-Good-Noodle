using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_AmmoController : MonoBehaviour
{
    [SerializeField] private TMP_Text currentAmmo;
    [SerializeField] private TMP_Text reserveAmmo;
    [SerializeField] private Image weaponSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary> Update when firing weapon </summary>
    public void UpdateAmmoUI(int currAmmo){
        currentAmmo.text = currAmmo.ToString();
    }

    /// <summary> Update when reloading weapon </summary>
    public void UpdateAmmoUI(int currAmmo, int resvAmmo){
        currentAmmo.text = currAmmo.ToString();
        reserveAmmo.text = "/ " + resvAmmo.ToString();
    }

    /// <summary> Update when switching weapon </summary>
    public void UpdateAmmoUI(Sprite weapon, int currAmmo, int resvAmmo){
        currentAmmo.text = currAmmo.ToString();
        reserveAmmo.text = "/ " + resvAmmo.ToString();
        weaponSprite.sprite = weapon;
    }
}
