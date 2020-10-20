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
   //     Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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
}