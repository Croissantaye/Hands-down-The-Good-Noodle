using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPointToMouse : MonoBehaviour
{
    private Transform arm;
    // USED WHEN LIMITING ANGLE OF ROTATION FOR ARM
    // [Range(30f, 179f)] public float ArmRotationAngleMax;

    // Start is called before the first frame update
    void Start()
    {
        arm = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 armToMouse = getArmDirection();
        // rotate arm to match armToMouse
        float angle = Mathf.Atan2(armToMouse.y, armToMouse.x) * Mathf.Rad2Deg;

        // transform.rotation = Quaternion.AngleAxis(AbsoluteValueOfAngle(angle), Vector3.forward);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public Vector3 getArmDirection(){
        // find the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // get the direction from arm to the mouse
        return (mousePosition - arm.position);
    }

    // USED WHEN WANTING TO LIMIT THE ANGLE OF ROTATION ON ARM
    // private float AbsoluteValueOfAngle(float angle){
    //     float absAngle;
    //     if(arm.parent.localScale.x > 0){
    //         absAngle = Mathf.Clamp(angle, -ArmRotationAngleMax, ArmRotationAngleMax);
    //     }
    //     else{
    //         absAngle = (-1 * Mathf.Sign(angle)) * (Mathf.Abs(angle) - 180);
    //         absAngle = Mathf.Clamp(absAngle, -ArmRotationAngleMax, ArmRotationAngleMax);
    //         absAngle = Mathf.Sign(absAngle) * (180 - Mathf.Abs(absAngle));
    //     }
    //     return absAngle;
    // }
}
