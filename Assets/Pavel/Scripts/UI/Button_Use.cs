using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Use : MonoBehaviour
{
    GameObject player, item;
    // Start is called before the first frame update


    // Update is called once per frame


    public void Click(){
        item.GetComponent<IIntercatable>().Use(player);
    }

    public void getData(GameObject _player, GameObject _item){
        player=_player;
        item=_item;
    }
}
