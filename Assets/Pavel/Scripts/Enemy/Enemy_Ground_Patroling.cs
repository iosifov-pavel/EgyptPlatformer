﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ground_Patroling : MonoBehaviour
{
    int health = 1;
    int dir = 1;
    float speed = 2f;
    Vector3 checkground;
    Vector3 checkwall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Check();
        Move();
    }

    void Check(){
       RaycastHit2D ground = CheckGround();
       RaycastHit2D wall = CheckWall();
       if(ground.collider==null || (wall.collider!=null && wall.collider.gameObject.tag=="Ground")){
           changeDirection();
       }
    }

    void Move(){
        transform.Translate(new Vector3(dir*speed*Time.deltaTime,0,0),Space.World);
    }

    void changeDirection(){
        if(dir==1){
            transform.eulerAngles = new Vector3(0,180,0);
            dir=-1;
        } else {
            transform.eulerAngles = new Vector3(0,0,0);
            dir=1;
        }

    }

    RaycastHit2D CheckGround()
    {    
        checkground = transform.position;
        checkground.x+=0.33f*dir;
        RaycastHit2D hit;
        hit =  Physics2D.Raycast(checkground,Vector3.down, 0.2f);
        Debug.DrawRay(checkground,Vector3.down*0.2f,Color.red,0.02f);
        return hit;   
    }

    RaycastHit2D CheckWall()
    {   
        checkwall = transform.position;
        checkwall.x+=0.33f*dir;
        RaycastHit2D hit;
        hit =  Physics2D.Raycast(checkwall,new Vector2(dir,0), 0.1f);
        Debug.DrawRay(checkwall,new Vector2(dir,0)*0.1f,Color.yellow,0.02f);
        return hit;   
    }
}
