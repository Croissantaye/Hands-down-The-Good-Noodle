using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs{
        public Vector3 gunEndPoint;
        public Vector3 shootPosition; 
    }
    private Transform aimTransform;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private Transform crosshair;

    private Vector3 aimDirection;
    private Vector3 mousePosition;

    public Vector3 getAimDirection(){
        return aimDirection;
    }

    public float getAimAngle(){
        return aimTransform.eulerAngles.z;
    }
    public Transform getGunPoint(){
        return gunPoint;
    }

    private void Awake(){
        aimTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Aiming();
        UpdateCrosshair();
    }

    private void Aiming(){
        aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles =  new Vector3(0, 0, angle);
        Debug.DrawLine(transform.position, mousePosition, Color.cyan);
    }

    private void UpdateCrosshair(){
        crosshair.position = mousePosition;
    }

    private void Shooting(){
        if(Input.GetMouseButtonDown(0)){
            OnShoot?.Invoke(this, new OnShootEventArgs {
                gunEndPoint = gunPoint.position,
                shootPosition = mousePosition,
            });
        }
    }
}
