using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Wall : MonoBehaviour
{
    // Start is called before the first frame update
    Player_Movement player_Movement;
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
            player_Movement = other.gameObject.GetComponent<Player_Movement>();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && (other.gameObject.layer==9 || other.gameObject.layer==10)){
            player_Movement.ResetJumpCount();
            player_Movement.SetMultiplier(new Vector2(0.6f,0.8f),-111);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && (other.gameObject.layer==9 || other.gameObject.layer==10)){
            player_Movement.ResetMultiplier();
            once_applied=false;
        }
    }
}
