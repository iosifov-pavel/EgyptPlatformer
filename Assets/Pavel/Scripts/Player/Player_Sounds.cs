using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player_Sounds : MonoBehaviour
{
    [SerializeField] private AudioClip damaged;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip jump;
    [SerializeField] AudioClip shot;
    [SerializeField] AudioClip coin;
    public static Player_Sounds sounds;
    AudioSource[] audioSource;
    AudioSource source;
    AudioSource step;
    float fixedVolume;
    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponents<AudioSource>();
        //source = audioSource[0];
        //step = audioSource[1];
        source = GetComponent<AudioSource>();
        fixedVolume = source.volume;
        sounds = this;
        //step.clip = steps;
    }

    // Update is called once per frame

    public void PlaySound(string src){
        switch(src){
            case "damage":
                source.volume = 0.45f;
                source.PlayOneShot(damaged);
                //source.volume = fixedVolume;
                break;
            case "death":
                source.volume = 0.55f;
                source.PlayOneShot(death);
                //source.volume = fixedVolume;
                break;
            case "jump":
                source.volume = 0.15f;
                source.PlayOneShot(jump);
                //source.volume = fixedVolume;
                break;
            case "shot":
                source.volume = 0.35f;
                source.PlayOneShot(shot);
                //source.volume = fixedVolume;
                break;
            case "coin":
                source.volume = 0.20f;
                source.PlayOneShot(coin);
                break;
                //source.volume = fixedVolume;
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
