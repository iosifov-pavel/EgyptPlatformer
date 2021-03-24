using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player=null;
    Transform playerTransform=null;
    Rigidbody2D playerRigidbody=null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag=="GroundCheck"){
            if(player == null){
                player = other.transform.parent.GetComponent<Movement>();
                playerTransform = other.transform.parent.transform;
                playerRigidbody = other.transform.parent.GetComponent<Rigidbody2D>();
            }
            player.SetNonPhysicMovement(true);
            playerTransform.SetParent(transform);
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            playerTransform.SetParent(transform);
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            player.SetNonPhysicMovement(false);
            playerTransform.SetParent(null);
        }
    }
}
