using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Conveyer : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float forceTime = 0f;
    string tittle = "conveyer";
    [SerializeField] int dir = -1;
    Vector2 force;
    Movement player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            force = new Vector2(speed*dir,0);
            player = other.transform.parent.gameObject.GetComponent<Movement>();
            //player.SetOtherSource(tittle,force,-1);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            //player.AddForces(force);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        //player.ResetOtherSource(tittle);
    }
}
