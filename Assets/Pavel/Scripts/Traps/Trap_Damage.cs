using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Damage : MonoBehaviour
{
    int damage = -1;
    Player_Health ph;
    Player_Movement player_Movement;
    Rigidbody2D rb;
    Transform tr;
    AudioSource source;
    // Start is called before the first frame update
    private void Start() {
        source = GetComponent<AudioSource>();
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

    private void Damage(GameObject other){
            if(other.tag=="Player" && other.layer==9 || other.layer == 10){
            ph = other.GetComponent<Player_Health>();
            player_Movement = other.GetComponent<Player_Movement>();
            if(ph.superman || ph.dead) return;
            if(source) source.Play();
            player_Movement.BlockMovement(0.25f);
            rb = other.GetComponent<Rigidbody2D>();
            float y=0;
            if(rb.velocity.y>0.1) y=1;
            else y=-1;
            tr = other.GetComponent<Transform>();
            Vector3 player_dir = new Vector3(Mathf.Sign(tr.localScale.x)*1,y,0);    
            player_dir*=-1;
            player_dir.Normalize();
            ph.ChangeHP(damage);
            rb.velocity = new Vector2(rb.velocity.x,0);
            rb.AddForce(player_dir*4,ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(source) Invoke("stopSound", 0.5f);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(source) Invoke("stopSound", 0.35f);
    }

    void stopSound(){
        source.Stop();
    }
}
