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
        if(other.gameObject.tag=="Player"){
            if(player == null){
                player = other.GetComponent<Movement>();
                playerTransform = other.transform;
                playerRigidbody = other.GetComponent<Rigidbody2D>();
            }
            player.SetNonPhysicMovement(true);
            other.gameObject.transform.SetParent(transform);
        } 
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player.SetNonPhysicMovement(false);
            other.gameObject.transform.SetParent(null);
        }
    }
}
