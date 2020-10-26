using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Jump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame


    void OnTriggerEnter2D(Collider2D other){
        Rigidbody2D rb = other.attachedRigidbody;
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.AddForce(transform.up*12f, ForceMode2D.Impulse);
    }
}
