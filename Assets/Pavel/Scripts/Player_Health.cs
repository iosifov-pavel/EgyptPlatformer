using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    int hp = 3;
    int MAXhp = 3;
    public bool isDamaged = false;
    public bool superman = false;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        hp=MAXhp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHP(int source){
        if(superman == true) return;
        hp+=source;
        Debug.Log("Health " + hp);
        StartCoroutine(damageIndication());
    }

    private IEnumerator damageIndication()
    {
        isDamaged = true;
        superman=true;
        SpriteRenderer player = this.gameObject.GetComponent<SpriteRenderer>();
        player.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        isDamaged=false;
        yield return new WaitForSeconds(1.7f);
        superman=false;
        player.color = Color.white;
    }
}
