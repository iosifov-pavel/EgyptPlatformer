using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Health : MonoBehaviour
{
    // Start is called before the first frame update
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
            player.GetComponent<Player_Health>().ChangeHP(1);
            Destroy(gameObject);
        }
    }
}
