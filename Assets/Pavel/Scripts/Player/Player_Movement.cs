﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    private float speed = 72f;
    private float maxSpeed = 3f;
    public Vector2 direction;
    Vector2 move;
    public Vector2 stick_delta;
    //---------------------------
    List<string> source_names = new List<string>();
    List<Vector2> sources = new List<Vector2>();
    List<int> source_times = new List<int>();
    //--------------------------
    float jump_force = 4f;
    float jump_time = -111f;
    float jump_time_max = 0.2f;
    int jump_count = 2;
    int jumps = 0;
    float enough_for_jump = 80;
    float enough_for_reset = 40;
    public bool isFalling=false;
    public bool isGrounded=true;
    public bool isJumping=false;
    bool lastcheck=false;
    private float gravity = 2.2f;
    bool can_jump=false;
    public Vector2 verical;
    //-------------------------------
    public bool onSlope = false;
    private float slopeangle;
    //----------------------------
    Rigidbody2D rb;
    Player_Health ph;
    Transform tran;
    BoxCollider2D checkground;
    Player_Animation anima;
    PhysicsMaterial2D normal;
    [SerializeField] PhysicsMaterial2D zero;
    //--------------------------------------
    public bool stickPressed = false;
    public bool blocked = false;
   // PhysicsMaterial2D OnSlope;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.drag=1f;
        tran = GetComponent<Transform>();
        ph = GetComponent<Player_Health>();
        checkground = tran.GetChild(1).gameObject.GetComponent<BoxCollider2D>();
        anima = GetComponent<Player_Animation>();
        normal = rb.sharedMaterial;
        verical=Vector2.zero;
        lastcheck=isGrounded;
    }

    // Update is called once per frame
    void Update(){
        if(stickPressed){
        } else {
             direction = new Vector2(0, 0);
             stick_delta = new Vector2(0,0);
             verical = new Vector2(0,0);
        }
        verical+=stick_delta;
        //if(rb.velocity.y<0 && !isJumping && !isGrounded) isFalling=true;
        anima.setDirection(direction.x);
        CheckGround();
        //Jump();

        anima.setBoolAnimation("Ground", isGrounded);   
    }

    void Jump(){
        if(isJumping) verical.x=0;
        if(verical.x>enough_for_jump){
            if(!isJumping && jumps<jump_count){
                can_jump=true;
                jumps++;
            }
            verical.x=0;
            verical.y=0;
            if(jump_time==-111) jump_time=jump_time_max;
        }

        if(can_jump){
           if(jump_time>=0) jump_time-=Time.deltaTime;
           else if(jump_time<0) can_jump=false;
        }

        if(verical.y>enough_for_reset){
            verical.x=0;
            verical.y=0;
            isJumping=false;
            //isFalling=false;
            can_jump=false;
            jump_time=-111;
        }
    }

    private void FixedUpdate() {
        if(ph.dead){
            CustomPhysics();
            return;
        }
        GetInput(); 
        if(!blocked){
        PreMove();
        Horizontal();
        Jump();
        Vertical();
        AdditionalMove();
        CustomPhysics();
        PostMove();
        }
    }

    void GetInput(){
        if( Mathf.Abs(direction.x )<0.2) direction.x = 0;
        move = new Vector2((direction.x)*Time.deltaTime*speed, rb.velocity.y);
    }

    void Horizontal(){
        rb.velocity = move;
        if (Mathf.Abs(move.x) > maxSpeed) {
            move = new Vector2(Mathf.Sign(move.x) * maxSpeed, rb.velocity.y);
        }

        anima.setFloatAnimation("Velocity",Mathf.Abs(rb.velocity.x));
        anima.setFloatAnimation("Direction",Mathf.Abs(direction.x));

        if((direction.x > 0 && tran.localScale.x < 0)||(direction.x < 0 && tran.localScale.x > 0)){
            Flip();
        }
    }

    void Vertical(){
        anima.setFloatAnimation("vSpeed",rb.velocity.y);
        if(can_jump){
            isJumping=true;  
            //Debbuger.Print("Jump");  
            rb.drag=2f;
            anima.setBoolAnimation("Ground",false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            if(jumps==2 && !isFalling) rb.AddForce(Vector3.up * (jump_force-1f), ForceMode2D.Impulse);
            else rb.AddForce(Vector3.up * jump_force, ForceMode2D.Impulse);
        }
    }

    void AdditionalMove(){
        Vector2 summary=Vector2.zero;
        if(source_names.Count<=0) return;
        foreach(string name in source_names){
            int i = source_names.IndexOf(name);
            if(source_times[i]==-1) summary+=sources[i];
            if(source_times[i]>0){
                summary+=sources[i];
                source_times[i]--;
            }
            if(source_times[i]==0){
                source_names.RemoveAt(i);
                sources.RemoveAt(i);
                source_times.RemoveAt(i); 
            }
        }
        rb.velocity+=summary;
    }

    void CheckGround(){  
        bool check=false;
        Collider2D[] hits = new Collider2D[10];
        Physics2D.OverlapCollider(checkground, new ContactFilter2D(),hits);
        foreach(Collider2D hit in hits){
            if(hit!=null && (hit.gameObject.tag=="Ground" || hit.gameObject.tag=="Trap")){
                isGrounded=true;
                check=true;
                isJumping=false;
                isFalling=false;
                jump_time=-111;
                jumps=0;
                slopeangle = Vector2.Angle(transform.up, hit.transform.up);
                if(slopeangle>=3f) onSlope=true;
                else onSlope = false;
                break;
            }
            check = false;
            onSlope = false;
            isGrounded=false;
        }
        if(!isJumping && lastcheck && !check){
            lastcheck=false;
            isFalling=true;
            jumps++;
        }
        lastcheck=check;
    }

    void Flip(){
        Vector3 thisScale = tran.localScale;
        thisScale.x *= -1;
        tran.localScale = thisScale;
    }

    void CustomPhysics(){
        bool directionchanged = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        bool needtostop = (rb.velocity.x!=0f && direction.x==0);
        if(isGrounded){
            if(ph.dead){
                rb.drag=8;
                return;
            } 
            rb.gravityScale = gravity;
            rb.drag=1f;
            if(directionchanged || !stickPressed || needtostop){
            }
        } else {
            rb.gravityScale = gravity;
            if(ph.dead){
                rb.drag=4;
                return;
            }
            rb.drag=2f;
        }
    }

    void PreMove(){
        if(onSlope && direction.x == 0 || transform.parent!=null){
            rb.sharedMaterial = null;
            }
        else if(!isGrounded){
            rb.sharedMaterial = zero;
        }
        else rb.sharedMaterial = normal;
    }

    void PostMove(){
        if(onSlope && direction.x==0){
        } 
    }

    public void SetOtherSource(string name, Vector2 source, int seconds){
        source_names.Add(name);
        sources.Add(source);
        source_times.Add(seconds);
    }

    public void ResetOtherSource(string name){
        if(sources.Count==0) return;
        int i =  source_names.IndexOf(name);
        source_names.RemoveAt(i);
        sources.RemoveAt(i);
        source_times.RemoveAt(i);
    }

    public void BlockMovement(float time){
        StartCoroutine(Block(time));
    }

    public void unBlock(){
        blocked=false;
        StopAllCoroutines();
    }

    IEnumerator Block(float time){
        blocked = true;
        yield return new WaitForSeconds(time);
        blocked = false;
    }
}