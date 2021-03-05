using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Toggle : MonoBehaviour,IIntercatable
{
    GameObject player;
    bool condition = false;
    Transform lamp;
    SpriteRenderer lamp_sprite;
    Transform child;
    [SerializeField] Color on;
    [SerializeField] Color off;
    IChild child_script;
    bool ready=true;
    // Start is called before the first frame update
    void Start()
    {
        lamp = transform.GetChild(1);
        try{
            child = transform.GetChild(2);
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

    public void Use(GameObject _player){
        if(!child_script.Done) return;
        player=_player;
        condition = !condition;
    }
}
