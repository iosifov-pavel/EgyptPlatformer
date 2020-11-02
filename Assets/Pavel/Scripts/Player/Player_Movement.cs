using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Health ph;
    private float speed = 20f;
    public float maxSpeed = 4f;
    public Vector2 direction;
    private float jump_force = 5f;
    private float jump_time = 0f;
    private float jump_max = 0.18f;
    public bool isGrounded = true;
    public bool onSlope = false;
    bool CanJump = false;
    private float gravity = 2.8f;
    private float slopeangle;
    Rigidbody2D rb;
    Transform tran;
    BoxCollider2D checkground;
    Player_Animation anima;
    PhysicsMaterial2D normal;
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
       // OnSlope.friction=400000f;
       // OnSlope.bounciness=0f;
    }

    // Update is called once per frame
    void Update(){
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        anima.setDirection(direction.x);
        CheckGround();

     //   if(Mathf.Abs(direction.x)>0 && isGrounded) GetComponent<Player_Sounds>().PlaySteps(true);
      //  else GetComponent<Player_Sounds>().PlaySteps(false);

        anima.setBoolAnimation("Ground", isGrounded);
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
        //    GetComponent<Player_Sounds>().PlaySound("jump");
            jump_time=jump_max;
            CanJump=true;
        }
        if(Input.GetKey(KeyCode.Space) && CanJump){
            jump_time-=Time.deltaTime;
            if(jump_time<=0) CanJump=false;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            jump_time=-1;
            CanJump=false;
        }
        
    }

    private void FixedUpdate() {
        if(ph.dead){
            CustomPhysics();
            return;
        } 
        if(!ph.isDamaged){
        PreMove();
        Horizontal();
        Vertical();
        CustomPhysics();
        }
    }

    void Horizontal(){
        rb.AddForce(new Vector2(direction.x*Time.deltaTime*speed, 0), ForceMode2D.Impulse);
        anima.setFloatAnimation("Speed",Mathf.Abs(direction.x));
        if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        if((direction.x > 0 && tran.localScale.x < 0)||(direction.x < 0 && tran.localScale.x > 0)){
            Flip();
        }
    }

    void Vertical(){
        anima.setFloatAnimation("vSpeed",rb.velocity.y);
        if(jump_time>=0 && CanJump){    
            rb.drag=2.5f;
            anima.setBoolAnimation("Ground",false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector3.up * jump_force, ForceMode2D.Impulse);
        }
    }

    void CheckGround(){  
    Collider2D[] hits = new Collider2D[10];
        Physics2D.OverlapCollider(checkground, new ContactFilter2D(),hits);
        foreach(Collider2D hit in hits){
            if(hit!=null && (hit.gameObject.tag=="Ground" || hit.gameObject.tag=="Trap")){
                isGrounded=true;
                slopeangle = Vector2.Angle(transform.up, hit.transform.up);
                if(slopeangle>=3f) onSlope=true;
                else onSlope = false;
                return;
            }
        }
        onSlope = false;
        isGrounded=false;
    }

    void Flip(){
        Vector3 thisScale = tran.localScale;
        thisScale.x *= -1;
        tran.localScale = thisScale;
    }

    void CustomPhysics(){
        bool directionchanged = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        bool needtostop = ((rb.velocity.x>0.1f || rb.velocity.x<-0.1f) && direction.x==0);
       // bool needtostop = (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D));
        if(isGrounded){
            if(ph.dead){
                rb.drag=8;
                return;
            } 
            rb.gravityScale = gravity;
            rb.drag=1f;
            if(directionchanged || needtostop){               
               // rb.velocity = new Vector2(0,rb.velocity.y);
               rb.drag=125f;
            }
        } else {
            rb.gravityScale = gravity;
            if(ph.dead){
                rb.drag=4;
                return;
            }
            rb.drag=2.5f;
        }
    }

    void PreMove(){
        if(onSlope && direction.x == 0){
            rb.sharedMaterial = null;
            }
        else rb.sharedMaterial = normal;
    }
}