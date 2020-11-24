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
    MonoBehaviour child_script;
    // Start is called before the first frame update
    void Start()
    {
        lamp = transform.GetChild(1);
        child = transform.GetChild(2);
        lamp_sprite = lamp.GetComponent<SpriteRenderer>();
        lamp_sprite.color = condition ? on : off;
        child_script = child.GetComponent<MonoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(condition){
            lamp_sprite.color = on;
        } else {
            lamp_sprite.color = off;
        }
    }

    public void Use(GameObject _player){
        player=_player;
        condition = condition==false ? true : false;
    }
}
