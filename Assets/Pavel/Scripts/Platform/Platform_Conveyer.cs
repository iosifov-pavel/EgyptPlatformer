using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Conveyer : MonoBehaviour
{
    float speed = 15f;
    int dir = -1;
    Rigidbody2D player;
    Vector2 force;
    // Start is called before the first frame update
    void Start()
    {
        force = new Vector2(dir,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player = other.gameObject.GetComponent<Rigidbody2D>();
            player.AddForce(force*speed,ForceMode2D.Force);
            //player.velocity = new Vector2(player.velocity.x+force.x,player.velocity.y);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            //player.AddForce(force*speed,ForceMode2D.Force);
           // player.velocity = new Vector2(player.velocity.x+force.x,player.velocity.y);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        player=null;
    }
}
