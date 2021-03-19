using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Jump : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float force=2;
    Force impulse;
    
    Movement player;
    // Update is called once per frame
    private void Start() {
        //impulse = new Force(transform.up*force, 0.12f);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="GroundCheck"){
        Rigidbody2D rb = other.attachedRigidbody;
        //other.transform.parent.gameObject.GetComponent<Player_Movement>().jumps = 1;
        //other.transform.parent.gameObject.GetComponent<Player_Movement>().inertia=0;
        player = other.transform.parent.gameObject.GetComponent<Movement>();
        player.ResetJumpCount();
        rb.velocity = new Vector2(rb.velocity.x/4,0);
        Vector2 dir = transform.up;
        dir = new Vector2(dir.x*force*2.4f,dir.y*force);
        player.SetImpulseForce(dir, 0.35f);
        StartCoroutine(jumpThings());
        //rb.AddForce(dir, ForceMode2D.Impulse);
        }
    }

    IEnumerator jumpThings(){
        player.BlockJump(true);
        yield return new WaitForSeconds(0.1f);
        player.BlockJump(false);
    }

}
