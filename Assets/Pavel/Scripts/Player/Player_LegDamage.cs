using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LegDamage : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb2;
    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="WeakSpot"){
            if(other.transform.position.y>transform.position.y) return;
            other.transform.parent.gameObject.GetComponent<Enemy_Health>().TakeDamage(1);
            rb2 = transform.parent.gameObject.GetComponent<Rigidbody2D>();
            rb2.velocity = new Vector2(rb2.velocity.x,0);
            rb2.AddForce(transform.up*8f, ForceMode2D.Impulse);
        }
    }
}
