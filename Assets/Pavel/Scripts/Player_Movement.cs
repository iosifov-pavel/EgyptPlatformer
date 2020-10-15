using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float speed = 20f;
    private float maxSpeed = 4f;
    private Vector2 direction;
    private float jump_force = 8f;
    public bool isGrounded = true;
    bool CanJump = false;
    bool isJump = false;
    private float fallmultiplier = 3.5f;
    public float gravity = 2f;
    Rigidbody2D rb;
    Transform tran;
    BoxCollider2D checkground;
    Animator anim;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.drag=1f;
        tran = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        checkground = tran.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update(){
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        CheckGround();
        if(isGrounded){
            if(Input.GetKeyDown(KeyCode.Space)){
                CanJump=true;
            }
            if(Input.GetKeyUp(KeyCode.Space)){
                CanJump=false;
            }
        }
    }

    private void FixedUpdate() {
        Horizontal();
        Vertical();
        CustomPhysics();
    }

    void Horizontal(){
        rb.AddForce(new Vector2(direction.x*Time.deltaTime*speed, 0), ForceMode2D.Impulse);
        anim.SetFloat("Speed",Mathf.Abs(direction.x));
        if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        if((direction.x > 0 && tran.localScale.x < 0)||(direction.x < 0 && tran.localScale.x > 0)){
            Flip();
        }
    }

    void Vertical(){
        anim.SetBool("Ground",isGrounded);
        anim.SetFloat("vSpeed",rb.velocity.y);
        if(!isGrounded) return;
        if(isGrounded && CanJump){      
            anim.SetBool("Ground",false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector3.up * jump_force, ForceMode2D.Impulse);
            CanJump=false;
            isJump = true;
        }
    }

    void CheckGround(){
        Collider2D[] hits = new Collider2D[10];
        Physics2D.OverlapCollider(checkground, new ContactFilter2D(),hits);
        foreach(Collider2D hit in hits){
            if(hit!=null && hit.gameObject.tag=="Ground"){
                isGrounded=true;
                isJump=false;
                return;
            }
        }
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
        if(isGrounded){
            rb.gravityScale = gravity;
            rb.drag=1f;
            if(directionchanged || needtostop){               
                rb.velocity = new Vector2(0,rb.velocity.y);
            }         
        }
        else{
            rb.drag=2.5f;
            if(!Input.GetButton("Jump") && isJump && rb.velocity.y>0){
                rb.gravityScale = gravity * fallmultiplier;
            }
            else if(rb.velocity.y<0){
                rb.gravityScale = gravity;
            }
        }
    }
}