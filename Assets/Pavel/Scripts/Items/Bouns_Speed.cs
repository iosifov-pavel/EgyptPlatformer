using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouns_Speed : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 5;
    Player_Movement pm;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            pm = other.GetComponent<Player_Movement>();
            pm.Speed_Up(time);
            Destroy(gameObject);
        }
    }
}
