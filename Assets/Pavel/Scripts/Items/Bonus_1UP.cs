using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_1UP : MonoBehaviour
{
    // Start is called before the first frame update
    //Player_Health player_Health;
    Manager_Level level;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D player) 
    {
        if (player.gameObject.tag == "Player")
        {
            level = player.gameObject.GetComponent<Player_InfoHolder>().getLM();
            level.manager_Game.game_info.Lives++;
            Destroy(gameObject);
        }
    }
}
