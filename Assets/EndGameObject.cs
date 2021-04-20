using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameObject : MonoBehaviour
{
    private bool IsBossDead;
    private BoxCollider2D collision;
    private SpriteRenderer spriteRenderer;

    private void OnEnable() {
        Enemy_TonyRigatoni.TonyDeath += OnBossDeath;
    }

    private void OnDisable() {
        Enemy_TonyRigatoni.TonyDeath -= OnBossDeath;
    }

    // Start is called before the first frame update
    void Start()
    {
        IsBossDead = false;
        collision = gameObject.GetComponent<BoxCollider2D>();
        collision.enabled = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnBossDeath(){
        IsBossDead = true;
        collision.enabled = true;
        spriteRenderer.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if(player){
            sendToEndCredits();   
        }
    }

    private void sendToEndCredits(){
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
    }

}
