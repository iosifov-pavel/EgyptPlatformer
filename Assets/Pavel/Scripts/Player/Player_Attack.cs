using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
[SerializeField] GameObject bullet_p;
[SerializeField] Player_Health player_Health;
private float timeBtwShots=0.3f;
public bool canAttack = true;
public bool isAttacking = false;
GameObject parent;
Transform partran;
bool up = false;
Vector3 forward, upward;
Player_Animation pa;
public bool buttonAttack=false;
public int bUp=1;
public float angle;
Manager_Level LM;
//public int kills=0;
//static bool killed=false;

   void Start() {
      LM = transform.parent.gameObject.GetComponent<Player_InfoHolder>().getLM();
      parent = transform.parent.gameObject;
      partran = parent.GetComponent<Transform>();
      forward = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);        upward = new Vector3(transform.localPosition.x,transform.localPosition.y+0.3f,transform.localPosition.z);
      pa = parent.GetComponent<Player_Animation>();
   }
   void Update (){
      if(player_Health.dead) return;
      transform.localPosition = forward;
      if (buttonAttack && canAttack){
         isAttacking=true;
         Shoot(angle);
         StartCoroutine(AtackTime());
      }
      pa.setBoolAnimation("Up", up);
      pa.setBoolAnimation("Attack", isAttacking);

      //if(killed){
      //   kills++;
      //   killed=false;
      //} 
   }
   public void Shoot(float angle_){
      GameObject b = Instantiate(bullet_p,transform.position, transform.rotation) as GameObject;
      b.GetComponent<Player_Bullet>().GetPosition(angle_, bUp, transform.parent.gameObject);  
   }
   IEnumerator AtackTime(){
      canAttack = false;
      yield return new WaitForSeconds(timeBtwShots);
      canAttack=true;
      isAttacking=false;
      up=false;
   }

   //public static void EnemyWasKilled(){
   //   killed=true;
   //}
}
