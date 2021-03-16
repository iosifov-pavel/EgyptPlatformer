using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass_Button : MonoBehaviour
{
    // Start is called before the first frame update
    Transform sprite;
    [SerializeField] Transform[] childrens;
    Vector3 position_off, position_on;
    Vector3 phase;
    bool on=false;
    float speed = 1;
    List<IChild> childs = new List<IChild>();
    //List<IChild> child_script;
    void Start()
    {
        foreach(Transform child in childrens){
            //int i = childs.IndexOf(child);
            childs.Add(child.gameObject.GetComponent<IChild>());
        }
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
            if(childs.Count==0) return;
            foreach(IChild child in childs){
                child.On = true;
            }
        }
        else{
            float step = speed*Time.deltaTime;
            sprite.localPosition = Vector2.MoveTowards(sprite.localPosition,position_off,step);
            if(childs.Count==0) return;
            foreach(IChild child in childs){
                child.On = false;
            }
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
