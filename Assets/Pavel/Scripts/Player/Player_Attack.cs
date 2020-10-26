using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
   public GameObject bullet;
   private float timeBtwShots;
   public float startTimeBtwShots;
   GameObject parent;
   Transform partran;
   bool up = false;
   Vector3 original;
   float prev_dir=1;

    void Start() {
        parent = transform.parent.gameObject;
        partran = parent.GetComponent<Transform>();
        original = transform.localPosition;
    }

   void Update ()
   {   
      CheckFlip(); 
      ShootDir();
      if (Input.GetButtonDown("Fire1"))
      {
         if(Input.GetKey(KeyCode.W)) up=true;
         Shoot(up);
         up=false;
      }
      prev_dir= 1* Mathf.Sign(partran.localScale.x);
   }

   void Shoot( bool up_)
   {
        GameObject b = Instantiate(bullet,transform.position, transform.rotation) as GameObject;
        b.GetComponent<Player_Bullet>().GetPosition(partran.localScale, up_);  
   }

   void ShootDir(){
     /*  if(Input.GetKey(KeyCode.W)  && changeddir){
           transform.localPosition = original;
        transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y+0.5f,transform.localPosition.z);
        changeddir=false;
       }*/
      /*  if(partran.localScale.x>0 && changeddir){
             transform.localPosition= original;
        transform.localPosition = new Vector3(transform.localPosition.x+0.3f,transform.localPosition.y,transform.localPosition.z);
        changeddir=false;
      } 
      else if(partran.localScale.x<0 && changeddir){
          transform.localPosition = original;
        transform.localPosition = new Vector3(transform.localPosition.x-0.3f,transform.localPosition.y,transform.localPosition.z);
        changeddir=false;
      } */
   }

   void CheckFlip(){
     /*  if(prev_dir!=1* Mathf.Sign(partran.localScale.x)){
           changeddir=true;
       }
       else changeddir=false;   */
   }
}
