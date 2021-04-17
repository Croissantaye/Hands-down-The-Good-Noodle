using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 playerPosition;
    private static LevelManager LevelManager;
    private bool hasBeenActivated;
    private int playerHealth;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite OffState;
    [SerializeField] private Sprite OnState;
    private AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioPlayer = GetComponent<AudioSource>();
        spriteRenderer.sprite = OffState;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerPlatformerController player = other.GetComponent<PlayerPlatformerController>();
        if(player && !hasBeenActivated){
            playerPosition = player.GetRBPosition();
            hasBeenActivated = true;
            spriteRenderer.sprite = OnState;
            audioPlayer.Play();
            playerHealth = player.GetHealthSystem().getMaxHealth();
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
