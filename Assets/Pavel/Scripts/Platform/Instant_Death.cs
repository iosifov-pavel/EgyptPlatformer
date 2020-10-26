using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instant_Death : MonoBehaviour
{
    int damage = -100;
    Player_Health ph;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.tag=="Player"){
            ph = other.gameObject.GetComponent<Player_Health>();
            ph.superman=false;
            ph.ChangeHP(damage);
        }
    }
}
