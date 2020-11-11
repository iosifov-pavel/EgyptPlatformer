using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Use : MonoBehaviour
{
    GameObject player, item;
    bool on=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(){
        if(!on){
            on=true;
            player.GetComponent<Player_Movement>().enabled=false;
           // player.GetComponent<Player_Interact>().interact=true;
        }
        else{
            on=false;
            player.GetComponent<Player_Movement>().enabled=true;
          //  player.GetComponent<Player_Interact>().interact=false;
        }


        float a = 1;
        
    }

    public void getData(GameObject _player, GameObject _item){
        player=_player;
        item=_item;
    }
}
