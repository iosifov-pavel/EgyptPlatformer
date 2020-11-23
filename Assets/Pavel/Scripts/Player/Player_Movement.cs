using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Health ph;
    private float speed = 140f;
    private float maxSpeed = 3f;
    public Vector2 direction;
    public float  otherSource = 0;
    private float jump_force = 4.8f;
    private float jump_time = 0f;
    private float jump_max = 0.18f;
    public bool isGrounded = true;
    public bool onSlope = false;
    bool CanJump = false;
    private float gravity = 2.2f;
    private float slopeangle;
    Rigidbody2D rb;
    Transform tran;
    BoxCollider2D checkground;
    Player_Animation anima;
    PhysicsMaterial2D normal;
    [SerializeField] PhysicsMaterial2D zero;
    public bool buttonJump = false;
    public bool stickPressed = false;
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
        if(stickPressed){
        } else direction = new Vector2(0, 0);
        
        anima.setDirection(direction.x);
        CheckGround();

     //   if(Mathf.Abs(direction.x)>0 && isGrounded) GetComponent<Player_Sounds>().PlaySteps(true);
      //  else GetComponent<Player_Sounds>().PlaySteps(false);

        anima.setBoolAnimation("Ground", isGrounded);
        if(isGrounded && buttonJump){
        //    GetComponent<Player_Sounds>().PlaySound("jump");
            jump_time=jump_max;
            CanJump=true;
        }
        if(buttonJump && CanJump){
            jump_time-=Time.deltaTime;
            if(jump_time<=0) CanJump=false;
        }
        if(!buttonJump){
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
        PostMove();
        }
    }

    void Horizontal(){
        if( Mathf.Abs(direction.x )<0.2) direction.x = 0;

       
        //Vector2 move = new Vector2((direction.x + 1.1f*otherSource/100)*Time.deltaTime*speed, 0);
        //rb.AddForce(move, ForceMode2D.Impulse);


        Vector2 move = new Vector2((direction.x)*Time.deltaTime*speed, rb.velocity.y);
        if (Mathf.Abs(move.x) > maxSpeed) {
            move = new Vector2(Mathf.Sign(move.x) * maxSpeed, rb.velocity.y);
        }
        rb.velocity= new Vector2(move.x + maxSpeed*otherSource/100,move.y);

        //Vector2 move = new Vector2(direction.x*Time.deltaTime*speed - rb.velocity.x+maxSpeed*otherSource/100, 0 );
        //rb.AddForce(move,ForceMode2D.Impulse);

        //Vector2 move = new Vector2(direction.x*Time.deltaTime*speed - rb.velocity.x+maxSpeed*otherSource/100, 0 );
        //rb.velocity+=move;

        anima.setFloatAnimation("Velocity",Mathf.Abs(rb.velocity.x));
        anima.setFloatAnimation("Direction",Mathf.Abs(direction.x));

        if((direction.x > 0 && tran.localScale.x < 0)||(direction.x < 0 && tran.localScale.x > 0)){
            Flip();
        }
    }

    void Vertical(){
        anima.setFloatAnimation("vSpeed",rb.velocity.y);
        if(jump_time>=0 && CanJump){    
            rb.drag=2f;
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
        bool needtostop = (rb.velocity.x!=0f && direction.x==0);
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
               if(otherSource!=0){}
               else rb.drag=125f;
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
    }
}