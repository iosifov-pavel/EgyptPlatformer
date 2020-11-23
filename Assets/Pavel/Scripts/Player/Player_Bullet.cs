using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour{
public float speed = 7f;
public int dmg = 1;
public float dir = 1;
private float lifetime = 0.8f;
float angle = 0;
bool rotated = false;
int direction = 1;
SpriteRenderer sprt;
  // Start is called before the first frame update
   void Start (){
      sprt = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        Quaternion rot = Quaternion.Euler(0,0,angle*direction);
        transform.rotation = rot;
        Color newc = sprt.color;
        newc.a=1;
        sprt.color=newc;
        float step =  speed * Time.deltaTime; 
        transform.Translate(Vector3.right*step*dir);
        lifetime-=Time.deltaTime;
        if(lifetime<=0) Destroy(gameObject);
  }


    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
      Debug.Log(hitInfo.name);
      if (hitInfo.name == "Player" || hitInfo.name == "GroundCheck") return;
      else if(hitInfo.tag == "Enemy"){
          Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
          enemy.TakeDamage(dmg);
          Destroy(gameObject);
        } else Destroy(gameObject);
     
    }

    public void GetPosition (Vector3 pstn, float _angle, int dir_rot)
    {
      angle = _angle;
      Vector3 pos = transform.localScale;
      pos.x *= Mathf.Sign(pstn.x);
      dir = 1*Mathf.Sign(pstn.x);
      transform.localScale = pos;
      direction=dir_rot;
    }
}
