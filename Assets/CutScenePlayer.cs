using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutScenePlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject menu;
    private bool IsPlaying;
    // Start is called before the first frame update
    void Start()
    {
        IsPlaying  = true;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Skip")){
            IsPlaying = false;
        }
        if((ulong)videoPlayer.frame + 1 >= videoPlayer.frameCount){
            IsPlaying = false;
        }
        if(!IsPlaying){
            switchToMenu();
        }
    }

    private void switchToMenu(){
        videoPlayer.enabled = false;
        gameObject.SetActive(false);
        menu.SetActive(true);
    }
}
