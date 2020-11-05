using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    Enemy_Damage ed;
    public int health = 1;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        ed = GetComponent<Enemy_Damage>();
    }

    // Update is called once per frame

    public void TakeDamage(int damage){
        ed.isDamaged = true;
        health-=damage;
        if(health<=0){
            dead=true;
            Death();
        }
    }

    public void Death(){
        Destroy(this.gameObject);
    }
}
