using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour{
public float speed = 7f;
Vector2 player_scale;
public int dmg = 1;
private float lifetime = 0.8f;
float angle = 0;
int direction = 1;
SpriteRenderer sprt;
Quaternion rot;
  // Start is called before the first frame update
   void Start (){
      sprt = GetComponent<SpriteRenderer>();
    }
    private void Update() {
     // if(player_scale.x<0) rot = Quaternion.Euler(0,0,180-angle*direction);
     // else 
        rot = Quaternion.Euler(0,0,angle*direction);
        transform.rotation = rot;
        Color newc = sprt.color;
        newc.a=1;
        sprt.color=newc;
        float step =  speed * Time.deltaTime; 
        transform.Translate(Vector3.right*step);
        lifetime-=Time.deltaTime;
        if(lifetime<=0) Destroy(gameObject);
  }


    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
      Debug.Log(hitInfo.name);
      if (hitInfo.name == "Player" || hitInfo.name == "GroundCheck") return;
      else if(hitInfo.tag=="Ground" && lifetime>=0.6f) return;
      else if(hitInfo.tag=="Ground") Destroy(gameObject);
      else if(hitInfo.tag == "Shield"){
          hitInfo.gameObject.GetComponent<Shield_Block>().ReduceDurab();
          Destroy(gameObject);
      }
      else if(hitInfo.tag == "Enemy"){
          Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
          enemy.TakeDamage(dmg);
          Destroy(gameObject);
        } 
    }

    public void GetPosition (float _angle, int dir_rot)
    {
      angle = _angle;
      direction=dir_rot;
    }
}
