using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CheckCeiling : MonoBehaviour
{
    // Start is called before the first frame update
    Player_Movement player_Movement;
    Rigidbody2D rb;
    void Start()
    {
        player_Movement = transform.parent.gameObject.GetComponent<Player_Movement>();
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Ground"){
            rb.velocity = new Vector2(rb.velocity.x,0);
            player_Movement.cant_jump=true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Ground"){
            rb.velocity = new Vector2(rb.velocity.x,0);
            //player_Movement.buttonJump=false;
        }
    }
}
