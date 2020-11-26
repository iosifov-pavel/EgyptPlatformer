﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LegDamage : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Enemy"){
            other.gameObject.GetComponent<Enemy_Health>().TakeDamage(1);
            transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up*12f, ForceMode2D.Impulse);
        }
    }
}
