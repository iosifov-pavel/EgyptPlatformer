using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shoot_Toggle : MonoBehaviour
{
    [SerializeField] Transform[] childrens;
    List<IChild> childs = new List<IChild>();
    [SerializeField] bool state=false;
    [SerializeField] bool needToBeDone = false;
    [SerializeField] bool withTimer = false;
    [SerializeField] float timer = 2f;
    AudioSource source;
    [SerializeField] AudioClip onSound, offSound;
    float time =0;
    bool counting = false;
    //float delay_time = 1f;
    //bool recently_activated = false;
    Transform lamp;
    SpriteRenderer lamp_sprite;
    [SerializeField] Color on;
    [SerializeField] Color off;
    //IChild child_script;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        lamp = transform.GetChild(0);
        lamp_sprite = lamp.GetComponent<SpriteRenderer>();
        lamp_sprite.color = state ? on : off;
        foreach(Transform child in childrens){
                //int i = childs.IndexOf(child);
                childs.Add(child.gameObject.GetComponent<IChild>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(childs.Count==0) return;
        if(withTimer && counting){
            time+=Time.deltaTime;
            if(time>=timer){
                counting = false;
                ChangeState();
            } 
        }
        lamp_sprite.color = state ? on : off;
        //if(needToBeDone){
        //    if(child_script.Done){
        //        if(state){
        //            child_script.On = true;
        //        } else {
        //            child_script.On = false;
        //        }
        //    }
        //}
        //else{
        //    if(state){
        //            child_script.On = true;
        //        } else {
        //            child_script.On = false;
        //        } 
        //}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //if(recently_activated) return;
        if(other.gameObject.tag=="Player_Bullet"){
            ChangeState();
            //StartCoroutine(Activate());
        }
    }


    public void ChangeState(){
        foreach(IChild child_script in childs){
            if(!child_script.Done && needToBeDone) return;
        }
        state=!state;
        if(state)source.PlayOneShot(onSound);
        else source.PlayOneShot(offSound);
        if(withTimer){
            if(state==true) counting=true;
            time = 0;
        }
        lamp_sprite.color = state ? on : off;
        foreach(IChild child_script in childs){
            if(state){
                child_script.On = true;
            } else {
                child_script.On = false;
            }
        }
            
    }
}
