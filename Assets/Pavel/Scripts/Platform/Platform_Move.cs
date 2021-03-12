﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour
{
    //public Vector3 p1;
    [SerializeField] Vector3 move_point;
    private Vector3 original,point;
    [SerializeField] private float speed = 1f;
    [SerializeField] float delay = 0;
    float timer = 0;
    Transform platform;
    bool start = false;

    
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        if(!start)Gizmos.DrawLine(transform.position, transform.position + move_point);
        else Gizmos.DrawLine(original, point);
    }

    // Start is called before the first frame update
    void Start()
    {
        platform = transform.parent;
        original = platform.transform.position;
        point = original + move_point;
        start = true;
    }

    bool GetReady(){
        timer+=Time.deltaTime;
        if(timer>=delay) return true;
        else return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetReady()) return;
        float step =  speed * Time.deltaTime; 
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, point, step);
        if(platform.transform.position==point){
            Vector3 temp = original;
            original = point;
            point = temp;
        }
    }

}

