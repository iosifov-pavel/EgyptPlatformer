using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    int hp;
    int MAXhp = 3;
    public bool isDamaged = false;
    public bool superman = false;
    public bool dead = false;
    Player_Animation anima;

    // Start is called before the first frame update
    void Start()
    {
        dead=false;
        hp=MAXhp;
        anima = GetComponent<Player_Animation>();
    }

    // Update is called once per frame


    public void ChangeHP(int source){
        if(superman || dead) return;
        hp+=source;
        Debug.Log("Health " + hp);
        if(hp<=0){
             Death();
             return;
        }
    //    GetComponent<Player_Sounds>().PlaySound("damage");
        StartCoroutine(damageIndication());
    }

    public void Death(){
        dead = true;
        StopAllCoroutines();
        anima.setBoolAnimation("Dead",dead);
//        GetComponent<Player_Sounds>().PlaySound("death");
        SpriteRenderer player = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        player.color = Color.red;
    }

    private IEnumerator damageIndication()
    {
        isDamaged = true;
        superman=true;
        SpriteRenderer player = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        player.color = Color.gray;
        anima.setBoolAnimation("Damaged",isDamaged);
        gameObject.layer=10;
        yield return new WaitForSeconds(0.2f);
        isDamaged=false;
        anima.setBoolAnimation("Damaged",isDamaged);
        yield return new WaitForSeconds(1.7f);
        gameObject.layer=9;
        superman=false;
        player.color = Color.white;
    }

    public void BecomeSuperman(float time){
        StartCoroutine(super(time));
    }

    IEnumerator super(float t){
        superman=true;
        yield return new WaitForSeconds(t);
        superman = false;
    }
}
