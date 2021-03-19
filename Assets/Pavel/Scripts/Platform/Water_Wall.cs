using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Wall : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player;
    [SerializeField] float slow_multiplier=0.8f;
    bool once_applied = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(once_applied) return;
        if(other.gameObject.tag == "Player" && (other.gameObject.layer==9 || other.gameObject.layer==10)){
            once_applied=true;
            player = other.gameObject.GetComponent<Movement>();
            Vector3 multi = new Vector3(slow_multiplier*0.8f,slow_multiplier*0.5f,slow_multiplier*0.4f);
            player.SetGravity(false,0.2f);
            player.SetMultiplierMovement(multi);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && (other.gameObject.layer==9 || other.gameObject.layer==10)){
            player.ResetJumpCount();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && (other.gameObject.layer==9 || other.gameObject.layer==10)){
            player.ResetMultiplier();
            player.RestoreGravity();
            once_applied=false;
        }
    }
}
