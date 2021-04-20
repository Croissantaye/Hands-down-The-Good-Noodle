using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_EndCredits : MonoBehaviour
{
    public void backToMainMenu(){
        SceneManager.LoadScene("Main_Menu_Test", LoadSceneMode.Single);
    }

    public void quitGame(){
        Application.Quit();
    }
}
