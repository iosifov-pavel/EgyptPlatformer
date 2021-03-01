using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky_Wall : MonoBehaviour
{
    // Start is called before the first frame update
    Player_Movement player_Movement;
    Player_Health player_Health;
    [SerializeField] Button_Jump button_Jump;
    public Vector2 x;
    Vector2 pre_push;
    Rigidbody2D rb_player;
    GameObject player;
    bool contact = false, ready = false, delay=false;
    float ready_time = 0.25f, delay_time=0.6f, timer=0;
    public Vector2 distanceV=Vector2.zero;
    public float distance=0;
    void Start()
    {
        pre_push=transform.right;
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
            x=new Vector2(player_Movement.hor,player_Movement.ver);
            float angle = Vector2.Angle(pre_push,x);
                if(player_Movement.buttonJump && x.magnitude<=50 || player_Health.isDamaged) Fall();
                else if(x.magnitude>50){
                    if(player_Movement.buttonJump) Jump();
                } 
        }
    }

    void Fall(){
        distance = 0;
        distanceV = Vector2.zero;
        player_Movement.jumps=0;
        timer=0;
        ready=false;
        contact=false;
        //rb_player.bodyType = RigidbodyType2D.Dynamic;
        rb_player.gravityScale = player_Movement.gravity;
        player.transform.parent = null;
        player_Movement.blocked=false;
        StartCoroutine(Delay());
    }

    void Jump(){
        distance = 0;
        distanceV = Vector2.zero;
        timer=0;
        player_Movement.jumps=1;
        ready=false;
        contact=false;
        player.transform.parent = null;
        //rb_player.bodyType = RigidbodyType2D.Dynamic;
        rb_player.gravityScale = player_Movement.gravity;
        x=x.normalized;
        if(x.y>-0.3f) rb_player.AddForce(new Vector2(x.x*8,x.y*12), ForceMode2D.Impulse);
        else rb_player.AddForce(new Vector2(x.x*6,x.y*4), ForceMode2D.Impulse);
        StartCoroutine(Delay());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(delay) return;
        if(other.gameObject.tag=="GrabWall" || other.gameObject.tag=="GrabCeiling"){
            x=Vector2.zero;
            player = other.gameObject.transform.parent.gameObject;
            player_Movement = player.GetComponent<Player_Movement>();
            player_Health = player.GetComponent<Player_Health>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            rb_player.gravityScale = 0;
            //rb_player.bodyType = RigidbodyType2D.Kinematic;
            player.transform.parent = transform;
            player_Movement.ResetJumpCount();
            player_Movement.blocked=true;
            player_Movement.jump_block=true;
            contact = true;
            player_Movement.verical=Vector2.zero;
            player_Movement.isJumping=false;
            distanceV =transform.position - player.transform.position;
            distance = (transform.position - player.transform.position).magnitude;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"){
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"|| other.gameObject.tag=="GrabCeiling"){
            float dis = (transform.position - player.transform.position).magnitude;
            if(dis>=distance+0.02f){
                if(contact && ready) Fall();
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
        //player_Movement.cant_jump = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(delay_time-0.25f);
        //player_Movement.cant_jump = false;
        delay=false;
    }
}
