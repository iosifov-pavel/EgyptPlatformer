using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dont_Kill_Them : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player_Health>().ChangeHP(-1);
    }
}
