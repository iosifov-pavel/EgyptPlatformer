using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    int health = 1;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage){
        health-=damage;
        if(health<=0){
            dead=true;
            Death();
        }
    }

    private void Death(){
        Destroy(this.gameObject);
    }
}
