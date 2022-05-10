using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public GameObject credits;
    public GameObject replayMenu;
    private bool onReplayMenu;

    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(true);
        replayMenu.SetActive(false);
        onReplayMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Skip") && !onReplayMenu){
            switchScreens();
        }
    }

    private void switchScreens(){
        credits.SetActive(false);
        replayMenu.SetActive(true);
        onReplayMenu = true;
    }
}
