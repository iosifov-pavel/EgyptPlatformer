using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name=="Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().superman = false;
            other.gameObject.GetComponent<PlayerHealth>().Hurt(100);
        }
    }
}
