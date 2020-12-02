using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky_Wall : MonoBehaviour
{
    // Start is called before the first frame update
    Player_Movement player_Movement;
    public Vector2 player_input;
    Vector2 pre_push;
    Rigidbody2D rb_player;
    GameObject player;
    bool contact = false, ready = false;
    float time = 0.2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(contact) time-=Time.deltaTime;
        if(contact && time<=0){
            ready = true;
        }
        if(ready){
            player_input = player_Movement.direction;
            if(Mathf.Abs(player_input.x)>1.8f || Mathf.Abs(player_input.x)>1.8f){

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"){
            player = other.gameObject.transform.parent.gameObject;
            player_Movement = player.GetComponent<Player_Movement>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            rb_player.bodyType = RigidbodyType2D.Static;
            player.transform.parent = transform;
            player_Movement.ResetJumpCount();
            player_Movement.blocked=true;
            contact = true;
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
}
