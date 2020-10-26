using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Damage : MonoBehaviour
{
    int damage = -1;
    Player_Health ph;
    Rigidbody2D rb;
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D other) {
        Damage(other);
    }
    private void OnCollisionStay2D(Collision2D other) {
        Damage(other);
    }

    private void Damage(Collision2D other){
            if(other.gameObject.tag=="Player"){
            ph = other.gameObject.GetComponent<Player_Health>();
            if(ph.superman || ph.dead) return;
            rb = other.gameObject.GetComponent<Rigidbody2D>();
            float y=0;
            if(rb.velocity.y>0.1) y=1;
            else y=-1;
            tr = other.gameObject.GetComponent<Transform>();
            Vector3 player_dir = new Vector3(Mathf.Sign(tr.localScale.x)*1,y,0);    
            player_dir*=-1;
            player_dir.Normalize();
            ph.ChangeHP(damage);
            rb.velocity = new Vector2(rb.velocity.x,0);
            rb.AddForce(player_dir*4,ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        
    }
}
