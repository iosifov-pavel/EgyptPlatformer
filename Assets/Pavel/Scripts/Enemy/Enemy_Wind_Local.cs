using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wind_Local : MonoBehaviour
{
   public float strength;
   public Vector2 diraction;

   public bool inWind = false;
   public GameObject windZone;

  Rigidbody2D rgdb;


   private void FixedUpdate() 
   {
       if(inWind)
       {
          rgdb.AddForce(diraction*strength);
       }
   }
   
   private void OnTriggerEnter2D(Collider2D other) 
   {
       if(other.gameObject.tag == "Player")
       {   
          rgdb= other.gameObject.GetComponent<Player_Movement>().GetRb();
          inWind = true;
       }
   }
   
   private void OnTriggerExit2D(Collider2D other) 
   {
       if(other.gameObject.tag == "Player")
       {   
           inWind = false;
       }   
   }
}
