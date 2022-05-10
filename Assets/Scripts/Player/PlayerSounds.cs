using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public List<AudioClip> actions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayByName(string name){
        foreach(AudioClip c in actions){
            if(c.name == name){
                StartCoroutine(PlaySound(c));
            }
        }
    }

    public void PlayByClip(AudioClip c){
        StartCoroutine(PlaySound(c));
    }

    IEnumerator PlaySound(AudioClip clip){
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(audio);
    }
}
