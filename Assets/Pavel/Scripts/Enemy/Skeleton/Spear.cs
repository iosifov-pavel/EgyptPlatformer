﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    float lifetime = 5f;
    public bool wasThrown = false;
    RigidbodyType2D rb;
    // Start is called before the first frame update
    void Start()
    { 
        rb = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        if(!wasThrown) return;
        if(GetComponent<Rigidbody2D>().bodyType==rb){
            lifetime-=Time.deltaTime;
        }
        if(lifetime<=0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Ground"){
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<BoxCollider2D>().enabled=false;
        }
    }
}
