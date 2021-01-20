using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shoot_Toggle : MonoBehaviour
{
    [SerializeField] bool state=false;
    float delay_time = 1f;
    bool recently_activated = false;
    Transform lamp,child;
    SpriteRenderer lamp_sprite;
    [SerializeField] Color on;
    [SerializeField] Color off;
    IChild child_script;
    // Start is called before the first frame update
    void Start()
    {
        lamp = transform.GetChild(0);
        child = transform.GetChild(1);
        lamp_sprite = lamp.GetComponent<SpriteRenderer>();
        lamp_sprite.color = state ? on : off;
        child_script = child.GetComponent<IChild>();
    }

    // Update is called once per frame
    void Update()
    {
        lamp_sprite.color = state ? on : off;
        if(child_script.Done){
            if(state){
                child_script.On = true;
            } else {
                child_script.On = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(recently_activated) return;
        if(other.gameObject.tag=="Player_Bullet"){
            ChangeState();
            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate(){
        recently_activated=true;
        yield return new WaitForSeconds(delay_time);
        recently_activated=false;
    }

    public void ChangeState(){
        if(child_script.Done){
            state=!state;
            lamp_sprite.color = state ? on : off;
            if(state){
                child_script.On = true;
            } else {
                child_script.On = false;
            }
        }
    }
}
