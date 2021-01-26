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
        off = transform.position;
        on = (Vector2)transform.position + new Vector2(0,-1.85f);
    }

    // Update is called once per frame
    void Update()
    {
        if(On){
            Done = false;
            float step =  speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, on, step);
            if((Vector2)transform.position==on) Done = true;
        } else {
            Done = false;
            float step =  speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, off, step);
            if((Vector2)transform.position==off) Done = true;
        }
        
    }
}
