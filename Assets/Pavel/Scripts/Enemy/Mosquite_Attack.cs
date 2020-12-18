using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquite_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    Player_Movement pm;
    Player_Health  ph;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            pm = other.gameObject.GetComponent<Player_Movement>();
            ph = other.gameObject.GetComponent<Player_Health>();
            ph.ChangeHP(-1);
            pm.SetMultiplier(0.5f,2);
        }
    }
}
