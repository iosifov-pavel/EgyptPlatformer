using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour{
[SerializeField] Sprite full, afterContact;
Collider2D col;
public float speed = 9f;
Vector2 player_scale;
public int dmg = 1;
private float lifetime = 0.9f;
float angle = 0;
int direction = 1;
SpriteRenderer sprt;
Quaternion rot;
public GameObject player;
bool wasContact = false;
  // Start is called before the first frame update
   void Start (){
      sprt = GetComponent<SpriteRenderer>();
      col = GetComponent<Collider2D>();
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
        if(lifetime<=0.8f && !wasContact) sprt.sprite = full; 
        if(lifetime<=0) Destroy(gameObject);
  }


    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
      Debug.Log(hitInfo.name);
      if (hitInfo.name == "Player" || hitInfo.name == "GroundCheck") return;
      else if(hitInfo.tag=="Ground" && lifetime>=0.8999f) return;
      else if(hitInfo.tag=="Ground" && lifetime<0.8999f) StartCoroutine(onContact());
      else if(hitInfo.tag=="Ground" && ! wasContact) StartCoroutine(onContact());
      else if(hitInfo.tag == "Shield" && ! wasContact){
          hitInfo.gameObject.GetComponent<Shield_Block>().ReduceDurab();
          StartCoroutine(onContact());
      }
      else if(hitInfo.tag == "Enemy" && ! wasContact){
          Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
          enemy.TakeDamage(dmg);
          StartCoroutine(onContact());
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
      if(other.gameObject.tag=="Ground"){
        if(lifetime<0.8999f && ! wasContact) StartCoroutine(onContact());
        else if(lifetime>=0.8999f) StartCoroutine(onContact());
      }
    }

    public void GetPosition (float _angle, int dir_rot, GameObject p)
    {
      player = p;
      angle = _angle;
      direction=dir_rot;
    }

    IEnumerator onContact(){
      wasContact = true;
      sprt.sprite = afterContact;
      speed=0.1f;
      col.enabled=false;
      yield return new WaitForSeconds(0.11f);
      Destroy(gameObject);
    }
}
