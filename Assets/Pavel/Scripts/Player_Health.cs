using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    int hp = 3;
    int MAXhp = 33;
    public bool isDamaged = false;
    public bool superman = false;
    bool dead = false;
    Player_Animation anima;
    // Start is called before the first frame update
    void Start()
    {
        hp=MAXhp;
        anima = GetComponent<Player_Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHP(int source){
        if(superman == true) return;
        hp+=source;
        Debug.Log("Health " + hp);
        if(hp<=0){
             Death();
             return;
        }
        StartCoroutine(damageIndication());
    }

    public void Death(){
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

    private IEnumerator damageIndication()
    {
        isDamaged = true;
        superman=true;
        SpriteRenderer player = this.gameObject.GetComponent<SpriteRenderer>();
        player.color = Color.red;
        anima.setBoolAnimation("Damaged",isDamaged);
        yield return new WaitForSeconds(0.2f);
        isDamaged=false;
        anima.setBoolAnimation("Damaged",isDamaged);
        yield return new WaitForSeconds(1.7f);
        superman=false;
        player.color = Color.white;
    }
}
