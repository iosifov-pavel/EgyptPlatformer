using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky_Wall : MonoBehaviour
{
    // Start is called before the first frame update
    Player_Movement player_Movement;
    public Vector2 x;
    Vector2 pre_push;
    Rigidbody2D rb_player;
    GameObject player;
    bool contact = false, ready = false, delay=false;
    float ready_time = 0.2f, delay_time=0.5f, timer=0;
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
                if(player_Movement.buttonJump && x.magnitude<=50) Fall();
                else if(x.magnitude>50){
                    if(player_Movement.buttonJump) Jump();
                } 
        }
    }

    void Fall(){
        player_Movement.jumps=0;
        timer=0;
        ready=false;
        contact=false;
        rb_player.bodyType = RigidbodyType2D.Dynamic;
        player.transform.parent = null;
        player_Movement.blocked=false;
        StartCoroutine(Delay());
    }

    void Jump(){
        timer=0;
        player_Movement.jumps=0;
        ready=false;
        contact=false;
        player.transform.parent = null;
        rb_player.bodyType = RigidbodyType2D.Dynamic;
        //rb_player.AddForce(x.normalized*(10+((x.magnitude-50)*4/100))  , ForceMode2D.Impulse);
        if(x.y<=10) rb_player.AddForce(x.normalized*(4+((x.magnitude-50)*2/100)), ForceMode2D.Impulse);
        else rb_player.AddForce(x.normalized*(10+((x.magnitude-50)*4/100))  , ForceMode2D.Impulse);
        //else rb_player.AddForce(x.normalized*9, ForceMode2D.Impulse);
        player_Movement.blocked=false;
        StartCoroutine(Delay());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(delay) return;
        if(other.gameObject.tag=="GrabWall" || other.gameObject.tag=="GrabCeiling"){
            x=Vector2.zero;
            player = other.gameObject.transform.parent.gameObject;
            player_Movement = player.GetComponent<Player_Movement>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            rb_player.bodyType = RigidbodyType2D.Static;
            player.transform.parent = transform;
            player_Movement.ResetJumpCount();
            player_Movement.blocked=true;
            player_Movement.jump_block=true;
            contact = true;
            player_Movement.verical=Vector2.zero;
            player_Movement.isJumping=false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"){
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"){

        }
    }

    IEnumerator Delay(){
        x=Vector2.zero;
        delay=true;
        yield return new WaitForSeconds(0.25f);
        player_Movement.jump_block=false;
        yield return new WaitForSeconds(delay_time-0.25f);
        delay=false;
    }
}
