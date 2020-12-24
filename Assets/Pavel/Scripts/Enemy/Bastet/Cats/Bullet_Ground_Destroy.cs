using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Ground_Destroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Ground"){
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Ground"){
            Destroy(gameObject);
        }
    }
}
