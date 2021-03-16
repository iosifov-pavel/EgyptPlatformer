using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Toggle : MonoBehaviour,IIntercatable
{
    GameObject player;
    bool condition = false;
    Transform lamp;
    SpriteRenderer lamp_sprite;
    [SerializeField] Transform child;
    [SerializeField] bool withTimer = false;
    [SerializeField] float timer = 2f;
    [SerializeField] Color on;
    [SerializeField] Color off;
    [SerializeField] bool needToBeDone = false;
    IChild child_script;
    bool ready=true;
    // Start is called before the first frame update
    void Start()
    {
        lamp = transform.GetChild(1);
        try{
            child_script = child.GetComponent<IChild>();
        }
        catch{}
        lamp_sprite = lamp.GetComponent<SpriteRenderer>();
        lamp_sprite.color = condition ? on : off;
    }

    // Update is called once per frame
    void Update()
    {
        if(child_script==null) return;
        if(needToBeDone){
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
        else{
            if(condition){
                    lamp_sprite.color = on;
                    child_script.On = true;
                } else {
                    lamp_sprite.color = off;
                    child_script.On = false;
                }
        }    
    }

    public void Use(GameObject _player){
        if(!child_script.Done && needToBeDone) return;
        player=_player;
        condition = !condition;
    }
}
