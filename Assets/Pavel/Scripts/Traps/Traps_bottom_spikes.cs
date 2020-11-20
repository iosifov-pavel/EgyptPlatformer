using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps_bottom_spikes : MonoBehaviour, ITrigger
{
    Transform spikes;
    // Start is called before the first frame update
    public bool triggered = false;
    bool warning = false;
    bool waited = false;
    bool is_up = false;
    float speed_up=6;
    float speed_down=1.5f;
    float wait=1;
    Vector3 up;
    Vector3 normal;
    void Start()
    {
        spikes = transform.GetChild(1);
        up = new Vector3(0,0.3f,spikes.position.z);
        normal = spikes.localPosition;
        spikes.GetComponent<BoxCollider2D>().enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!triggered) return;
        if(!warning){
            spikes.localPosition=new Vector3(spikes.localPosition.x,spikes.localPosition.y+0.1f,spikes.localPosition.z);
            warning=true;
            StartCoroutine(waiting());
        }
        if(waited){
            float step =  speed_up * Time.deltaTime; 
            spikes.localPosition = Vector3.MoveTowards(spikes.localPosition, up, step);
            if(spikes.localPosition==up) StartCoroutine(pause());
        }
        if(is_up){
            float step =  speed_down * Time.deltaTime; 
            spikes.localPosition = Vector3.MoveTowards(spikes.localPosition, normal, step);
            if(spikes.localPosition==normal) StartCoroutine(restart());
        }
    }

    public void triggerAction(bool t){
        triggered=t;
    }

    IEnumerator waiting(){
        yield return new WaitForSeconds(wait);
        waited = true;
        spikes.GetComponent<BoxCollider2D>().enabled=true;
    }

    IEnumerator pause(){
        yield return new WaitForSeconds(wait/2);
        is_up=true;
        waited=false;
    }

    IEnumerator restart(){
        spikes.GetComponent<BoxCollider2D>().enabled=false;
        yield return new WaitForSeconds(wait);
        triggered=false;
        warning=false;
        waited=false;
        is_up=false;
        StopAllCoroutines();
    }
}
