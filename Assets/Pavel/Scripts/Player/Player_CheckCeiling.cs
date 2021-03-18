using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CheckCeiling : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player;
    Rigidbody2D rb;
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<Movement>();
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Ground"){
            rb.velocity = new Vector2(rb.velocity.x,0);
            //player.jumpBlock=true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Ground"){
        }
    }
}
