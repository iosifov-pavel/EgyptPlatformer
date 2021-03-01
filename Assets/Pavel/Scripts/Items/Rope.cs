using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called before the first frame update
    Player_Movement player_Movement;
    Player_Health player_Health;
    public Vector2 x;
    Vector2 pre_push;
    Rigidbody2D rb_player;
    GameObject player;
    bool contact = false, ready = false, delay=false;
    float ready_time = 0.25f, delay_time=0.6f, timer=0;
    [SerializeField] bool straight = true;
    [SerializeField] Button_Jump button_Jump;
    Vector2 forward;
    float end_max,end_min;
    BoxCollider2D box;
    public float distance=0;
    void Start()
    {
        forward = transform.up;
        box = GetComponent<BoxCollider2D>();
        if(straight){
            end_max = box.bounds.max.y;
            end_min = box.bounds.min.y; 
        }
        else{
            end_max = box.bounds.max.x;
            end_min = box.bounds.min.x; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(delay || !contact) return;
        pre_push=transform.right;
        if(contact) timer+=Time.deltaTime;
        if(contact && timer>=ready_time && !ready){
            ready = true;
            x=Vector2.zero;
        }
        if(ready){
            x = player_Movement.direction;
            if(player_Health.isDamaged){
                x=Vector2.zero;
                Jump();
            }
            if(player_Movement.buttonJump){
                Jump();
                return;
            }
            Vector3 move;
            if(straight){
                move = new Vector3(0,x.y,0) * Time.deltaTime;
                if(player.transform.position.y+move.y >= end_max) return;
                if(player.transform.position.y+move.y <= end_min) return;
                player.transform.Translate(move);
            }
            else{
                move = new Vector3(x.x,0,0) * Time.deltaTime;
                if(player.transform.position.x+move.x >= end_max) return;
                if(player.transform.position.x+move.x <= end_min) return;
                player.transform.Translate(move);
            }
        }
    }

    void Jump(){
        distance = 0;
        timer=0;
        player_Movement.jumps=0;
        ready=false;
        contact=false;
        player.transform.parent = null;
        player_Movement.jumps=1;
        player_Movement.isJumping = true;
        //rb_player.bodyType = RigidbodyType2D.Dynamic;
        rb_player.gravityScale = player_Movement.gravity;
        x=x.normalized;
        if(x.y>-0.3f) rb_player.AddForce(new Vector2(x.x*8,x.y*12), ForceMode2D.Impulse);
        else rb_player.AddForce(new Vector2(x.x*6,x.y*4), ForceMode2D.Impulse);
        StartCoroutine(Delay());
    }

     private void OnTriggerEnter2D(Collider2D other) {
        if(delay || contact) return;
        if(other.gameObject.tag=="GrabWall" || other.gameObject.tag=="GrabCeiling"){
            if(!straight && other.gameObject.tag=="GrabWall") return;
            if(other.gameObject.tag=="GrabCeiling" && straight) return;
            x=Vector2.zero;
            player = other.gameObject.transform.parent.gameObject;
            player_Movement = player.GetComponent<Player_Movement>();
            bool yep = player_Movement.isJumping || player_Movement.isFalling;
            if(!yep) return;
            player.transform.parent = transform;
            player_Health = player.GetComponent<Player_Health>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            rb_player.gravityScale = 0;
            //rb_player.bodyType = RigidbodyType2D.Kinematic;
            player_Movement.ResetJumpCount();
            player_Movement.inertia=0;
            player_Movement.blocked=true;
            player_Movement.jump_block=true;
            contact = true;
            player_Movement.verical=Vector2.zero;
            player_Movement.isJumping=false;
            if(straight){
                distance = Mathf.Abs(transform.position.x - player.transform.position.x);
            }
            else{
                
                distance = Mathf.Abs(transform.position.y - player.transform.position.y);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(delay || contact) return;
        if(other.gameObject.tag=="GrabWall" || other.gameObject.tag=="GrabCeiling"){
            if(!straight && other.gameObject.tag=="GrabWall") return;
            if(other.gameObject.tag=="GrabCeiling" && straight) return;
            x=Vector2.zero;
            player = other.gameObject.transform.parent.gameObject;
            player.transform.parent = transform;
            player_Movement = player.GetComponent<Player_Movement>();
            if(!player_Movement.isJumping) return;
            player_Health = player.GetComponent<Player_Health>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            //rb_player.bodyType = RigidbodyType2D.Kinematic;
            player_Movement.ResetJumpCount();
            player_Movement.inertia=0;
            player_Movement.blocked=true;
            player_Movement.jump_block=true;
            contact = true;
            player_Movement.verical=Vector2.zero;
            player_Movement.isJumping=false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"|| other.gameObject.tag=="GrabCeiling"){
            if(contact && ready){
                float dis;
                if(straight){
                    dis = Mathf.Abs(transform.position.x - player.transform.position.x);
                }
                else{
                    dis = Mathf.Abs(transform.position.y - player.transform.position.y);
                }
                if(dis>=distance+0.02f){
                    x=Vector2.zero;
                    Jump();
                }
            }
        }
    }
    IEnumerator Delay(){
        x=Vector2.zero;
        delay=true;
        player_Movement.buttonJump = false;
        yield return new WaitForSeconds(0.15f);
        player_Movement.blocked=false;
        player_Movement.jump_block=false;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(delay_time-0.25f);
        delay=false;
    }
}
