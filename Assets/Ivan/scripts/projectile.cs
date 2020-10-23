using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
  // public float speed;
  // public float lifeTime;
  // public float distance;
  // public int damage;
  // public LayerMask whatIsSolid;

  // 

  // private void Start()
  // {
  //    
  // }

  // private void Update()
  // {
  //     RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
  //     if (hitInfo.collider != null) {
  //         if (hitInfo.collider.CompareTag("Enemy")) {
  //             hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
  //         }
  //         DestroyProjectile();
  //     }


  //     transform.Translate(Vector2.up * speed * Time.deltaTime);
  // }

  // void DestroyProjectile() {
  //     Instantiate(transform.position, Quaternion.identity);
  //     Destroy(gameObject);
  // }

  public float speed = 5f;
  public int dmg = 1;
  public Rigidbody2D rb;

  void Start ()
  {
    rb.velocity = transform.right * speed;
  }


    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
      //Debug.Log(hitInfo.name);
      Enemy_Health enemy = hitInfo.GetComponent<Enemy_Health>();
      if( enemy != null)
      {
        enemy.TakeDamage(dmg);
      }
      Destroy(gameObject);
    }
}
