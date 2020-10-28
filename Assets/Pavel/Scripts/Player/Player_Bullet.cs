using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
 public float speed = 5f;
  public int dmg = 1;
  public float dir = 1;
  private float lifetime = 0.8f;
  bool up = false;
  bool rotated = false;
    // Start is called before the first frame update
     void Start ()
  {

  }
    private void Update() {
        if(up && !rotated){
            transform.Rotate(0,0,90*dir);
            rotated=true;
        } 
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

    public void GetPosition (Vector3 pstn, bool _up)
    {
        up = _up;
      Vector3 pos = transform.localScale;
      pos.x *= Mathf.Sign(pstn.x);
      dir = 1*Mathf.Sign(pstn.x);
      transform.localScale = pos;
    }
}
