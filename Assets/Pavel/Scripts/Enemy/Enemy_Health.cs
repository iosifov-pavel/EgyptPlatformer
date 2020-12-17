using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int health = 1;
    bool dead = false;
    bool is_damaged;
    Color original;
    SpriteRenderer[] sprites;
    float time=0.3f;
    // Start is called before the first frame update
    void Start(){
        sprites=GetComponentsInChildren<SpriteRenderer>();
        original=sprites[0].color;
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
        Manager_Level.EnemyWasKilled();
        Destroy(this.gameObject);
    }

    IEnumerator ReactToDamage(){
        is_damaged=true;
        foreach(SpriteRenderer s in sprites){
            s.color=Color.red;
        }
        yield return new WaitForSeconds(time);
        foreach(SpriteRenderer s in sprites){
            s.color=original;
        }
        is_damaged=false;
    }
}
