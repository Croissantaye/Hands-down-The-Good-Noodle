using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public GameObject door;
    private BoxCollider2D DoorCollision;
    private bool IsActivated;
    public GameObject finalBoss;

    private void OnEnable() {
        PlayerPlatformerController.Killed += resetDoor;
    }
    private void OnDisable() {
        PlayerPlatformerController.Killed -= resetDoor;
    }

    // Start is called before the first frame update
    void Start()
    {
        DoorCollision = door.GetComponent<BoxCollider2D>();
        DoorCollision.enabled = false;
        IsActivated = false;
        door.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if(!IsActivated && player){
            cam2.Priority = cam1.Priority + 1;
            IsActivated = true;
            door.SetActive(true);
            DoorCollision.enabled = true;
            finalBoss.GetComponent<Enemy_TonyRigatoni>().SetDirection(Vector3.left);
            finalBoss.GetComponent<Enemy_TonyRigatoni>().SetCanDie(true);
        }
    }

    private void resetDoor(int a){
        cam2.Priority = cam1.Priority - 1;
        DoorCollision.enabled = false;
        IsActivated = false;
        door.SetActive(false);
    }
}
