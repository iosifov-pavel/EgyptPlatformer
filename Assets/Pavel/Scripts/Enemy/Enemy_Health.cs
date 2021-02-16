using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int health = 1;
    public bool dead = false;
    public bool is_damaged;
    Color original;
    SpriteRenderer[] sprites;
    float time=0.3f;
    // Start is called before the first frame update
    void Start(){
        sprites=GetComponentsInChildren<SpriteRenderer>();
        if(sprites==null){}
        else original=sprites[0].color;
    }

    // Update is called once per frame

    public void TakeDamage(int damage){
        if(dead) return;
        if(is_damaged) return;
        health-=damage;
        StartCoroutine(ReactToDamage());
        if(health<=0){
            Death();
        }
    }

    public void Death(){
        //if(dead) return;
        dead = true;
        Manager_Level.EnemyWasKilled();
        Boss_Health boss_Health = gameObject.GetComponent<Boss_Health>();
        if(boss_Health!=null){
            boss_Health.FinishLevel();
            gameObject.SetActive(false);
        }
        else{
            Animator death_a = GetComponent<Animator>();
            if(death_a!=null){
                death_a.SetTrigger("death");
            }
            else Destroy(this.gameObject);
        }
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
