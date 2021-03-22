using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite calm, warning, attack;
    [SerializeField] float warning_time, up_time, after_time;
    [SerializeField] AudioClip ups, downs;
    float timer=0;
    bool trigered=false, up=false, ready=true;
    BoxCollider2D box;
    SpriteRenderer spriteRenderer;
    AudioSource source;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        box.enabled=false;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trigered){
            spriteRenderer.sprite = warning;
            timer+=Time.deltaTime;
            if(timer>=warning_time){
                trigered=false;
                up=true;
                timer=0;
                source.PlayOneShot(ups);
            }
        }
        if(up){
            timer+=Time.deltaTime;
            spriteRenderer.sprite = attack;
            box.enabled=true;
            if(timer >= up_time){
                spriteRenderer.sprite = calm;
                box.enabled=false;
                timer=0;
                up=false;
                source.PlayOneShot(downs);
            }
        }
        if(!trigered && !up){
            timer+=Time.deltaTime;
            if(timer>=after_time){
                ready=true;
                timer=0;
            }
        }
    }

    public void Triggered(){
            if(!ready) return;
            trigered = true;
            timer=0;
            ready = false;
    }
}
