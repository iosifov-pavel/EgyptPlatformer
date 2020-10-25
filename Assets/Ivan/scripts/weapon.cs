using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
   public Transform firePoint;
   public GameObject bullet;
   public int dmg = 1;
   public LineRenderer LR;
   private float timeBtwShots;
   public float startTimeBtwShots;
   public float speed = 5f;


   void Update ()
   {
      if (Input.GetButtonDown("Fire1"))
      {
         Shoot();
      }
   }

   void Shoot()
   {
     GameObject b = Instantiate(bullet,firePoint.position, firePoint.rotation) as GameObject;
      b.GetComponent<Projectile>().GetPosition(transform.localScale);  
   }
}