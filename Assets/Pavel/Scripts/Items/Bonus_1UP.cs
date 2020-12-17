using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_1UP : MonoBehaviour
{
    // Start is called before the first frame update
    Player_Health player_Health;
    bool yes=false;
    //Manager_Level level;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D player) 
    {   
        if(yes) return;
        if (player.gameObject.tag == "Player")
        {
            yes = true;
            player.gameObject.GetComponent<Player_Health>().OneUp();
            Destroy(gameObject);
        }
    }
}
