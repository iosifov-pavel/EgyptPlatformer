using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_See_You : MonoBehaviour
{
    Enemy_Attack enemy;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        enemy = parent.GetComponent<Enemy_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            enemy.isTrigered=true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
           enemy.isTrigered=true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            enemy.isTrigered=false;
        }
    }
}
