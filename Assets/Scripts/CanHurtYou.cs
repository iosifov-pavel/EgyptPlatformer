﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHurtYou : MonoBehaviour
{
    int dmg = 1;
    private ContactPoint2D[] contacts = new ContactPoint2D[1];
    Vector3 dir;
    public bool contact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    

    private void OnCollisionStay2D(Collision2D other) 
    {
        contact = true;
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if(player.superman) return;
        //Debug.Log("triger");
        other.GetContacts(contacts);
        //Vector2 pnt = other.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position);
        Vector2 pnt = contacts[0].point;
        Vector3 playerpos = other.gameObject.GetComponent<Transform>().position;
        dir = new Vector3(pnt.x-playerpos.x,pnt.y-playerpos.y,playerpos.z-playerpos.z);
        dir.Normalize();
        player.Hurt(dmg);  
        StartCoroutine(playerGetHit(other));     
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        contact = false;
    }

    
    private IEnumerator playerGetHit(Collision2D player) 
    {
        player.gameObject.GetComponent<CharacterControl>().isDamaged = true;
        player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(player.gameObject.GetComponent<Rigidbody2D>().velocity.x,0);
        player.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(dir.x*(-3f),dir.y*(-4.5f),dir.z),ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        player.gameObject.GetComponent<CharacterControl>().isDamaged = false;
    }

  
}
