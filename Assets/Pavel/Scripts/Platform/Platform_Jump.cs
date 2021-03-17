using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Jump : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float force=14;
    // Update is called once per frame


    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="GroundCheck"){
        Rigidbody2D rb = other.attachedRigidbody;
        other.transform.parent.gameObject.GetComponent<Player_Movement>().jumps = 1;
        other.transform.parent.gameObject.GetComponent<Player_Movement>().inertia=0;
        rb.velocity = new Vector2(0,0);
        rb.AddForce(transform.up*force, ForceMode2D.Impulse);
        //other.transform.parent.gameObject.GetComponent<Player_Movement>().BlockMovement(0.02f);
        }
    }
}
