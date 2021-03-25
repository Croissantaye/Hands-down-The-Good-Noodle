using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 playerPosition;
    private static LevelManager LevelManager;
    private bool hasBeenActivated;
    private int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerPlatformerController player = other.GetComponent<PlayerPlatformerController>();
        if(player && !hasBeenActivated){
            playerPosition = player.GetRBPosition();
            hasBeenActivated = true;
            playerHealth = player.GetCurrentHealth();
            LevelManager.setLastCheckpoint(this);
        }
    }

    public Vector3 GetPlayerPosition(){
        return playerPosition;
    }

    public int GetPlayerHealth(){
        return playerHealth;
    }
}
