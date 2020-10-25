using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon1 : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotPoint.rotation = Quaternion.Euler(0,0,-270);

           if (Input.GetKeyDown(KeyCode.W)) 
           {
               GameObject w = Instantiate(bullet, shotPoint.position, Quaternion.identity) as GameObject;
               //w.GetComponent<Projectile1>().GetPosision(transform.localScale);
               
           }
    
    }
}
