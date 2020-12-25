using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass_Button : MonoBehaviour
{
    // Start is called before the first frame update
    Transform sprite;
    Vector3 position_off, position_on;
    Vector3 phase;
    bool on=false;
    float speed = 2;
    void Start()
    {
        sprite = transform.GetChild(0);
        position_off = sprite.localPosition;
        phase = GetComponent<BoxCollider2D>().bounds.extents;
        position_on = new Vector3(position_off.x, position_off.y - phase.y*1.5f,5);
    }

    // Update is called once per frame
    void Update()
    {
        if(on){
            float step = speed*Time.deltaTime;
            sprite.localPosition = Vector2.MoveTowards(sprite.localPosition,position_on,step);
            //do something
        }
        else{
            float step = speed*Time.deltaTime;
            sprite.localPosition = Vector2.MoveTowards(sprite.localPosition,position_off,step);
            //undo something
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Ground") return;
        else{
            on=true;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Ground") return;
        else{
            on=true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Ground") return;
        else{
            on=false;
        }
    }
}
