using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int dmg)
    {
        health-=dmg;
        StartCoroutine(damageIndication());
        if(health<=0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator damageIndication()
    {
        SpriteRenderer player = this.gameObject.GetComponent<SpriteRenderer>();
        player.color = Color.red;
        yield return new WaitForSeconds(1);
        player.color = Color.white;
    }
}
