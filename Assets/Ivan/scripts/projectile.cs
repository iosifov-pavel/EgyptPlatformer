using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public float speed = 5f;
  public int dmg = 1;
  public Rigidbody2D rb;
  public float dir = 1;
  private float lifetime = 1.2f;

  void Start ()
  {

  }

   private void Update() {
    rb.velocity = new Vector2(transform.localScale.x  *  speed , 0f) ;
    lifetime-=Time.deltaTime;
        if(lifetime<=0) Destroy(gameObject);
  }


    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
      Debug.Log(hitInfo.name);
      if (hitInfo.name == "Player")
        return;
      Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
      if( enemy != null)
      {
        enemy.TakeDamage(dmg);
      }
      Destroy(gameObject);
    }

    public void GetPosition (Vector3 pstn)
    {
      Vector3 pos = transform.localScale;
      pos.x *= Mathf.Sign(pstn.x);
      dir = 1*Mathf.Sign(pstn.x);
      transform.localScale = pos;
    }

}
