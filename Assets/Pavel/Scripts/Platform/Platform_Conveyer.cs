using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Conveyer : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float forceTime = 0.5f;
    [SerializeField] bool isImpulse = false;
    string tittle = "conveyer";
    [SerializeField] int dir = -1;
    Vector2 force;
    Movement player;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update() {
            force = new Vector2(speed*dir,0);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            player = other.transform.parent.gameObject.GetComponent<Movement>();
            if(!isImpulse) player.SetConstantForce(tittle,force);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            if(isImpulse) player.SetImpulseForce(force,forceTime);
            //player.AddForces(force);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            if(!isImpulse) player.RemoveForce(tittle);
        }
    }
}
