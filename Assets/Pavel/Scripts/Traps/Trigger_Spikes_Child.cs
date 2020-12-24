using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Spikes_Child : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject parent;
    Trigger_Spikes trigger_Spikes;
    void Start()
    {
        parent = transform.parent.gameObject;
        trigger_Spikes = parent.GetComponent<Trigger_Spikes>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        trigger_Spikes.Triggered();
    }

    private void OnTriggerStay2D(Collider2D other) {
        trigger_Spikes.Triggered();
    }
}
