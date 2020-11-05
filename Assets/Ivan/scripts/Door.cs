using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
            Teleport(other.gameObject);
        
        
    }


    public void Teleport (GameObject Player)
    {   
       // Player_Movement plar  = Player.GetComponent<Player_Movement>(); 
        Player.transform.position = door.transform.position;
    }
}
