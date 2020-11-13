using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Conveyer : MonoBehaviour
{
    float percent = 50f;
    int dir = -1;
    Player_Movement player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
           // player = other.gameObject.GetComponent<Transform>();

            player = other.gameObject.GetComponent<Player_Movement>();
           // player.AddForce(force*speed,ForceMode2D.Force);
            //player.velocity = new Vector2(player.velocity.x+force.x,player.velocity.y);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
           // player.Translate(force*speed*Time.deltaTime);

            player.otherSource = dir*percent;
           // player.velocity = new Vector2(player.velocity.x+force.x,player.velocity.y);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        player.otherSource = 0;
    }
}
