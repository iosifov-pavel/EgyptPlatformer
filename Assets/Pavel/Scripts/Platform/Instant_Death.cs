using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instant_Death : MonoBehaviour
{
    int damage = -100;
    Player_Health ph;
    // Start is called before the first frame update
    // Update is called once per frame

    void KillEverything(Collider2D other){
        if(other.gameObject.tag=="Player" && other.gameObject.layer==9 || other.gameObject.layer==10){
            ph = other.gameObject.GetComponent<Player_Health>();
            ph.superman=false;
            ph.ChangeHP(damage);
        }
        else if(other.gameObject.tag=="WeakSpot"){
            Destroy(other.transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        KillEverything(other);
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        KillEverything(other);
    }
}
