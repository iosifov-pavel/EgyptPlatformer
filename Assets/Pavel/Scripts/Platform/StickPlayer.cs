using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player;
    Rigidbody2D playerRigidbody;
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
            player = other.GetComponent<Movement>();
            playerRigidbody = other.GetComponent<Rigidbody2D>();
            //playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
            //playerRigidbody.velocity = Vector2.zero;
            //playerRigidbody.gravityScale = 0;
            other.gameObject.transform.SetParent(transform);
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
           Vector2 move = player.GetInput();
           if(Mathf.Abs(move.x)>1.5f){
               other.gameObject.transform.SetParent(null);
           }
           else{
               other.gameObject.transform.SetParent(transform);
           }
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(null);
            //playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
            //player.RestoreGravity();
        }
    }
}
