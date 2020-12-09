using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    bool active = false;
    int id = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(active) return;
        if(other.gameObject.tag=="Player"){
            active=true;
            other.gameObject.GetComponent<Player_Health>().SetCheckPoint(transform,id);
        }
    }

    public int getID(){
        return id;
    }
}
