using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sand : MonoBehaviour
{    
    BoxCollider2D BC2d;

    Enemy_Damage dmg;

    Player_Health ph;

    Player_Movement pm;

    bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        BC2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D other) {

        
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "PLayer")
        {
            pm.GetComponent<Player_Movement>().below();
        }
        
    }


    IEnumerator SizeBox (float t)
    {
        pm.GetComponent<Player_Movement>().below();
        ph = GetComponent<Player_Health>();
        //if(ph.superman || ph.dead) return;
         yield return new WaitForSeconds(t);
        
    }

}
