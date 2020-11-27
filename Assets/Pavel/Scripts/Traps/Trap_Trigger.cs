using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Trigger : MonoBehaviour
{
    Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            parent.GetComponent<ITrigger>().triggerAction(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            parent.GetComponent<ITrigger>().triggerAction(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            //
        }
    }
}
