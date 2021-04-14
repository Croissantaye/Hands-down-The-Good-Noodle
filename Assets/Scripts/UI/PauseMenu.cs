using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] private GameObject player;
    private PlayerPlatformerController playerController;
    
    
    public void Resume()
        {
            pauseMenuUI.SetActive(false);
            playerController.EnableWeapons();
            Time.timeScale = 1f;
            IsGamePaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            playerController.DisableWeapons();
            Time.timeScale = 0;
            IsGamePaused = true;
        }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerPlatformerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        
    }
}
