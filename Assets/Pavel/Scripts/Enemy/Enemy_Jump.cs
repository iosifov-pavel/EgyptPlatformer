using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Jump : MonoBehaviour
{
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float jumpSpeed = 6f;
    [SerializeField] float jumpDelay = 1f;
    Enemy_Ground_Patroling1 enemy_Ground_Patroling1;
    float curr_add_speed=0;
    Transform point_to_jump;
    float distance_to_point;
    Vector2 up,down, destination_up, destination_down, forward;
    Rigidbody2D rb;
    int dir = 1;
    bool isJumping = false, is_up=true;
    Vector3 p1,p2,p3,p4;
    [SerializeField] float time = 1f;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        point_to_jump = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
        enemy_Ground_Patroling1 = GetComponent<Enemy_Ground_Patroling1>();
    }

    // Update is called once per frame
    void Update()
    {
        SeekForJump();
        if(isJumping){
            //Jump();
            Jump2();
            CheckGround();
        }
    }

    void SeekForJump(){
        if(Input.GetButtonDown("Jump")) CalculateJump();
    }

    void CalculateJump(){
        timer=0;
        dir = (int)Mathf.Sign(transform.localScale.x) * 1;
        distance_to_point = (point_to_jump.position.x + transform.position.x)/2;
        up = new Vector2(distance_to_point,transform.position.y + jumpHeight) - (Vector2)transform.position;
        down = (Vector2)point_to_jump.position - new Vector2(distance_to_point,transform.position.y + jumpHeight);
        destination_up = (Vector2)transform.position + up;
        destination_down = destination_up + down;
        is_up=true;
        enemy_Ground_Patroling1.enabled=false;
        isJumping=true;
        curr_add_speed = jumpSpeed;
        rb.bodyType = RigidbodyType2D.Dynamic;
        forward = Vector2.up;

        p1 = transform.position;
        p2 = transform.position + new Vector3(0,jumpHeight,0);
        p3 = point_to_jump.position + new Vector3(0,jumpHeight,0);
        p4 = point_to_jump.position;
    }

    void Jump(){
        Vector2 dest;
        if(is_up){
            dest = destination_up;
            curr_add_speed *= 0.95f;
            float speed = jumpSpeed+curr_add_speed;
            rb.position = Vector3.MoveTowards(rb.position,dest, speed*Time.deltaTime);
            if((Vector2)transform.position==destination_up){
                is_up=false;
            } 
        }
        else{
            dest = destination_down;
            rb.position = Vector3.MoveTowards(rb.position,dest, jumpSpeed*Time.deltaTime);
            if((Vector2)transform.position==destination_down){
                isJumping=false;
            } 
        }
    }

    void Jump2(){
        Vector3 new_pos = Bezier.GetPoint(p1,p2,p3,p4,timer);
        transform.position = new_pos;
        timer+=Time.deltaTime*jumpSpeed;
    }

    void CheckGround(){
        if(rb.velocity.y<0) {
            //rb.bodyType = RigidbodyType2D.Kinematic;
            //up=Vector2.zero;
        }
    }
}
