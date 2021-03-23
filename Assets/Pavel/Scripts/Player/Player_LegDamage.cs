using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LegDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Movement player_Movement;
    [SerializeField] Player_Health player_Health;
    Rigidbody2D rb2;
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="WeakSpot"){
            if(other.transform.position.y>transform.position.y) return;
            rb2 = transform.parent.gameObject.GetComponent<Rigidbody2D>();
            if(rb2.velocity.y>0) return;
            other.transform.parent.gameObject.GetComponent<Enemy_Health>().TakeDamage(3);
            rb2.velocity = new Vector2(rb2.velocity.x,0);
            player_Movement.SetImpulseForce(Vector2.up*3.3f, 0.35f);
            StartCoroutine(noDamage());
            player_Movement.ResetJumpCount();
        }
        else if(other.gameObject.tag == "Trap"){
            player_Movement.BlockJump(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Trap"){
            player_Movement.BlockJump(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Trap"){
            player_Movement.BlockJump(false);
        }
    }

    IEnumerator noDamage(){
        player_Health.afterHeadJump = true;
        yield return new WaitForSeconds(0.25f);
        player_Health.afterHeadJump = false;
    }
}
