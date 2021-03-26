using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundWalking : MonoBehaviour
{
    [SerializeField] Transform start, end, body;
    [SerializeField] bool vertical = false;
    [SerializeField] float speed = 2;
    [SerializeField] float waitTime = 2f;
    [SerializeField] float gravityScale = 1;
    [SerializeField] Vector2 gravityDirection = Vector2.down;
    float timer=0;
    float startPoint, endPoint;
    int dir = 1;
    //float gravity = -9.81f;
    Rigidbody2D rb;
    Vector2 move;
    Vector3 oldPosition;
    [SerializeField] bool forward = true;
    // Start is called before the first frame update
    void Start()
    {
        if(vertical){
            startPoint = start.position.y;
            endPoint = end.position.y;
        }
        else{
            startPoint = start.position.x;
            endPoint = end.position.x;
        }
        rb = body.GetComponent<Rigidbody2D>();
        oldPosition = body.position;
    }

    // Update is called once per frame
    void Update()
    {
        try{
            Check();
            Move();
            CheckWalls();
        }
        catch{
            if(body==null) Destroy(gameObject);
        }
    }

    private void CheckWalls()
    {
        if(body.position==oldPosition){
            timer+=Time.deltaTime;
            if(timer>=waitTime){
                forward=!forward;
                dir*=-1;
                timer=0;
            }
        }
        oldPosition = body.position;
    }



    private void Move()
    {
        if(vertical) rb.velocity = new Vector2(rb.velocity.x,dir*speed);
        else rb.velocity = new Vector2(dir*speed,rb.velocity.y);
        //Debug.Log(Physics2D.gravity);
        Vector2 gravityV = gravityDirection*gravityScale*(Physics2D.gravity.magnitude)*Time.deltaTime;
        rb.velocity+=gravityV;
    }

    private void Check()
    {
        if(vertical){
            if(body.position.y>=endPoint){
                forward = false;
                dir=-1;
            } 
            else if(body.position.y<=startPoint){
                forward = true;
                dir=1;
            }
        }
        else{
            if(body.position.x>=endPoint){
                forward = false;
                dir=-1;
            } 
            else if(body.position.x<=startPoint){
                forward = true;
                dir=1;
            }
        }

    }
}
