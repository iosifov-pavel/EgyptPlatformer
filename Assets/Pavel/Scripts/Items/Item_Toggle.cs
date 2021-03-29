using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Toggle : MonoBehaviour,IIntercatable
{
    GameObject player;
    bool condition = false;
    Transform lamp;
    SpriteRenderer lamp_sprite;
    [SerializeField] Transform[] childrens;
    List<IChild> childs = new List<IChild>();
    [SerializeField] bool withTimer = false;
    [SerializeField] float timer = 2f;
    [SerializeField] Color on;
    [SerializeField] Color off;
    [SerializeField] bool needToBeDone = false;
    AudioSource source;
    [SerializeField] AudioClip onSound, offSound;
    bool ready=true;
    float time=0;
    bool counting = false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        lamp = transform.GetChild(1);
        try{
            foreach(Transform child in childrens){
                //int i = childs.IndexOf(child);
                childs.Add(child.gameObject.GetComponent<IChild>());
            }
        }
        catch{}
        lamp_sprite = lamp.GetComponent<SpriteRenderer>();
        lamp_sprite.color = condition ? on : off;
    }

    // Update is called once per frame
    void Update()
    {
        if(childs.Count==0) return;
        if(withTimer && counting){
            time+=Time.deltaTime;
            if(time>=timer){
                counting = false;
                condition = !condition;
                if(condition)source.PlayOneShot(onSound);
                else source.PlayOneShot(offSound);
            }
        }
        if(needToBeDone){
            foreach(IChild child_script in childs){
                    if(child_script.Done){
                        if(condition){
                            lamp_sprite.color = on;
                            child_script.On = true;
                        } else {
                            lamp_sprite.color = off;
                            child_script.On = false;
                        }
                }
            } 
        }
        else{
            foreach(IChild child_script in childs){
                if(condition){
                        lamp_sprite.color = on;
                        child_script.On = true;
                    } else {
                        lamp_sprite.color = off;
                        child_script.On = false;
                    }
            }
        }    
    }

    public void Use(GameObject _player){
        foreach(IChild child_script in childs){
            if(!child_script.Done && needToBeDone) return;
        }
        player=_player;
        condition = !condition;
        if(condition)source.PlayOneShot(onSound);
        else source.PlayOneShot(offSound);
        if(withTimer && condition){
            time=0;
            counting=true;
        }
    }
}
