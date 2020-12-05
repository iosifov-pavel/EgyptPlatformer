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
    float ready_time = 0.3f, delay_time=0.5f, timer=0;
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
            if(angle>95) return;
            else{
                if(x.magnitude>110){
                    if(x.y<-90) Fall();
                    else Jump();
                }
            }
        }
    }

    void Fall(){
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
        ready=false;
        contact=false;
        player_Movement.verical=new Vector2(50,10);
        rb_player.bodyType = RigidbodyType2D.Dynamic;
        player.transform.parent = null;
        rb_player.AddForce(x.normalized*9, ForceMode2D.Impulse);
        player_Movement.blocked=false;
        StartCoroutine(Delay());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(delay) return;
        if(other.gameObject.tag=="GrabWall"){
            x=Vector2.zero;
            player = other.gameObject.transform.parent.gameObject;
            player_Movement = player.GetComponent<Player_Movement>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            rb_player.bodyType = RigidbodyType2D.Static;
            player.transform.parent = transform;
            player_Movement.ResetJumpCount();
            player_Movement.blocked=true;
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
        yield return new WaitForSeconds(delay_time);
        delay=false;
    }
}
