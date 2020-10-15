using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] UI userin;
    private int health = 3;
    private int MAXhealth = 3;
    public bool superman = false;
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        health=MAXhealth;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int dmg)
    {
        if(superman==true) return;
        health-=dmg;
        userin.changeHealth(dmg);
        Debug.Log("Health " + health);
        StartCoroutine(damageIndication());
        if(health<=0)
        {
            Death();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMAXHealth()
    {
        return MAXhealth;
    }

    public void Death()
    {
        dead=true;
        Destroy(this.gameObject);
        userin.deathscreen();
    }

    private IEnumerator damageIndication()
    {
        superman = true;
        SpriteRenderer player = this.gameObject.GetComponent<SpriteRenderer>();
        player.color = Color.red;
        yield return new WaitForSeconds(1.7f);
        superman = false;
        player.color = Color.white;
    }
}
