using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int health = 1;
    bool dead = false;
    bool is_damaged;
    Color original;
    SpriteRenderer sprite;
    float time=0.3f;
    //float time_invincible=0.2f;
    // Start is called before the first frame update
    void Start(){
        sprite=GetComponent<SpriteRenderer>();
        original=sprite.color;
    }

    // Update is called once per frame

    public void TakeDamage(int damage){
        if(is_damaged) return;
        health-=damage;
        StartCoroutine(ReactToDamage());
        if(health<=0){
            dead=true;
            Death();
        }
    }

    public void Death(){
        Destroy(this.gameObject);
    }

    IEnumerator ReactToDamage(){
        is_damaged=true;
        sprite.color=Color.red;
        yield return new WaitForSeconds(time);
        sprite.color=original;
        is_damaged=false;
    }
}
