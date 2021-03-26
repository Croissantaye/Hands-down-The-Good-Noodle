using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    // public delegate void OnKillPlayer();
    // public static event OnKillPlayer Killed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerPlatformerController Player = other.GetComponent<PlayerPlatformerController>();
        // Debug.Log("enter killzoe");
        if(Player){
            Debug.Log("kill player");
            Player.Die();
        }
    }
}
