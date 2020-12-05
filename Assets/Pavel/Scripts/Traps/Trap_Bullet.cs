using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Bullet : MonoBehaviour
{
    //Transform parent;
    float speed = 6;
    float life_time = 2;
    Vector3 move;
    Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        //parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        life_time-=Time.deltaTime;
        if(life_time<=0) Destroy(gameObject);
        move = vector * speed * Time.deltaTime;
        transform.position+=move;
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.tag=="Ground") {
            Destroy(gameObject);
        } 
    }

    public void GetDirection(Vector3 v){
        vector=v;
    }
}
