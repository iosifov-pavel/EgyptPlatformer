using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player_Sounds : MonoBehaviour
{
    [SerializeField] private AudioClip damaged;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip steps;
    AudioSource[] audioSource;
    AudioSource source;
    AudioSource step;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        source = audioSource[0];
        step = audioSource[1];
        step.clip = steps;
    }

    // Update is called once per frame

    public void PlaySound(string src){
        switch(src){
            case "damage": 
                source.PlayOneShot(damaged);
                break;
            case "death":
                source.PlayOneShot(death);
                break;
            case "jump":
            source.PlayOneShot(jump);
            break;
        }
    }

    public void PlaySteps(bool on){
        if(on && !step.isPlaying) {
            step.Play();
        } else if(!on) {
            step.Stop();
        }
        else{

        }
        
    }
}
