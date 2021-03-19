using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind2 : MonoBehaviour
{
    // Start is called before the first frame update
    string id= "wind";
    [SerializeField] Vector2 direction = Vector2.up;
    Movement player;
    [SerializeField] bool isImpulse=false;
    [SerializeField]float time = 0.4f;
    [SerializeField] float strength=1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player = other.gameObject.GetComponent<Movement>();
            if(!isImpulse)player.SetConstantForce(id,direction*strength);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            if(isImpulse) player.SetImpulseForce(direction*strength,time);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            if(!isImpulse)player.RemoveForce(id);
        }
    }
}
