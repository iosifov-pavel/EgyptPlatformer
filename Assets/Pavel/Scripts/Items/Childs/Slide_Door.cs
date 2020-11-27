using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide_Door : MonoBehaviour, IChild
{
    public bool On{get;set;} = false;
    public bool Done{get;set;} = true;
    float speed = 5f;
    Vector2 off, on;
    // Start is called before the first frame update
    void Start()
    {
        off = transform.localPosition;
        on = new Vector2(2.2f,-1.85f);
    }

    // Update is called once per frame
    void Update()
    {
        if(On){
            Done = false;
            float step =  speed * Time.deltaTime; 
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, on, step);
            if((Vector2)transform.localPosition==on) Done = true;;
        } else {
            Done = false;
            float step =  speed * Time.deltaTime; 
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, off, step);
            if((Vector2)transform.localPosition==off) Done = true;;
        }
        
    }
}
