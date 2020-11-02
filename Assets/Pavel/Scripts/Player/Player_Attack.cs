using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
   public GameObject bullet;
   private float timeBtwShots=0.4f;
   public bool canAttack = true;
   public bool isAttacking = false;
   GameObject parent;
   Transform partran;
   bool up = false;
   Vector3 forward, upward;
   Player_Animation pa;
   public bool buttonAttack=false;

    void Start() {
        parent = transform.parent.gameObject;
        partran = parent.GetComponent<Transform>();
        forward = new Vector3(transform.localPosition.x+0.15f,transform.localPosition.y,transform.localPosition.z);
        upward = new Vector3(transform.localPosition.x,transform.localPosition.y+0.3f,transform.localPosition.z);
        pa = parent.GetComponent<Player_Animation>();
    }

   void Update (){   
      if(Input.GetKey(KeyCode.W)){
         transform.localPosition = upward;
      } 
      else transform.localPosition = forward;

      if (buttonAttack && canAttack){
         isAttacking=true;
         if(Input.GetKey(KeyCode.W)) up=true;
         Shoot(up);
         StartCoroutine(AtackTime());
      }
      pa.setBoolAnimation("Up", up);
      pa.setBoolAnimation("Attack", isAttacking);
   }

   public void Shoot(bool up_){
        GameObject b = Instantiate(bullet,transform.position, transform.rotation) as GameObject;
        b.GetComponent<Player_Bullet>().GetPosition(partran.localScale, up_);  
   }

   IEnumerator AtackTime(){
         canAttack = false;
         yield return new WaitForSeconds(timeBtwShots);
         canAttack=true;
         isAttacking=false;
         up=false;
   }
}
