using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Jump : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame


    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="GroundCheck"){
        Rigidbody2D rb = other.attachedRigidbody;
        rb.velocity = new Vector2(rb.velocity.x/4,0);
        rb.AddForce(transform.up*14f, ForceMode2D.Impulse);
        }
    }
}
