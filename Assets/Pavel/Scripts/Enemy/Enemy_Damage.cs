using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    // Start is called before the first frame update
    int damage = -1;
    public bool isDamaged=false;
    Player_Health ph;
    Enemy_Health enemy_Health;
    Rigidbody2D rb;
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        enemy_Health = GetComponent<Enemy_Health>();
    }
    // Update is called once per frame


    private void OnCollisionEnter2D(Collision2D other) {
        Damage(other.gameObject);
    }
    private void OnCollisionStay2D(Collision2D other) {
        Damage(other.gameObject);
    }
    private void OnTriggerStay2D(Collider2D other) {
        Damage(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Damage(other.gameObject);
    }

    public void Damage(GameObject other){
            try{
                if(enemy_Health.is_damaged || enemy_Health.dead){
                    return;
                } 
            }
            catch{
                Debug.Log("ERRROORRRRR!!!! "+gameObject);
            }
            if(other.tag=="Player" && (other.layer==9 || other.layer == 10)){
                ph = other.GetComponent<Player_Health>();
                if(ph.superman || ph.dead) return;
                rb = other.GetComponent<Rigidbody2D>();
                float y=0;
                if(rb.velocity.y>0.1) y=1;
                else y=-1;
                Vector3 player_dir = new Vector3(Mathf.Sign(other.transform.localScale.x)*1,y,0);    
                player_dir*=-1;
                player_dir.Normalize();
                ph.ChangeHP(damage);
                rb.velocity = new Vector2(0,0);
                rb.AddForce(player_dir*4,ForceMode2D.Impulse);
            }  
    }

    private void OnCollisionExit2D(Collision2D other) {
        
    }
}
