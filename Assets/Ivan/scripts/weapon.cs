using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
   // public GameObject projectile;
   // public GameObject shotEffect;
   // public Transform shotPoint;
//
   // private float timeBtwShots;
   // public float startTimeBtwShots;
//
//
   // //Враг
   // public int health;
   // public GameObject deathEffect;
   // public GameObject explosion;
//
   // private void Update()
   // {
   //     
   //     Vector3 difference = Camera.main.ScreenToWorldPoint(Input.KeyDown(KeyCode('W'))) - transform.position;
   //     float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
   //     transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
//
   //     if (timeBtwShots <= 0)
   //     {
   //         if (Input.GetMouseButton(0))
   //         {
   //             Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
   //             // тут анимация 
   //             Instantiate(projectile, shotPoint.position, transform.rotation);
   //             timeBtwShots = startTimeBtwShots;
   //         }
   //     }
   //     else {
   //         timeBtwShots -= Time.deltaTime;
   //     }
//
//
//
//
   //     //враг
   //     if (health <= 0) {
   //         Instantiate(deathEffect, transform.position, Quaternion.identity);
   //         Destroy(gameObject);
   //     }
   // 
//
   // public void TakeDamage(int damage) {
   //     //Анимация
   //     Instantiate(explosion, transform.position, Quaternion.identity);
   //     health -= damage;
   // }
   // }


   public Transform firePoint;
   public GameObject bullet;
   public int dmg = 1;
   public LineRenderer LR;


   void Update ()
   {
      if (Input.GetButtonDown("Fire1"))
      {
         Shoot();
         //StartCoroutine(Shoot());
      }
   }

   void Shoot()
   //IEnumerator Shoot()
   {
     GameObject b = Instantiate(bullet,firePoint.position, firePoint.rotation) as GameObject;
      b.GetComponent<Projectile>().GetPosition(transform.localScale);
  //    RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
  //    if (hit) transform.parent.transform.localScale
  //    {
  //       Debug.Log(hit.transform.name);
  //       Enemy_Health enemy = hit.transform.GetComponent<Enemy_Health>();
  //          if( enemy != null)
  //          {
  //             enemy.TakeDamage(dmg);
  //          }
//
  //           LR.SetPosition(0, firePoint.position);
  //           LR.SetPosition(1, hit.point);
  //    } else 
  //    {
  //          LR.SetPosition(0, firePoint.position);
  //           LR.SetPosition(1, firePoint.position + firePoint.right * 100);
  //    }
       //  LR.enable = true;
        // yield return new WaitForSeconds(1f);
         //LR.enable = false;
  //    
  }
}