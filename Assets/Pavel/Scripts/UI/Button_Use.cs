using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Use : MonoBehaviour
{
    GameObject player, item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(){
        item.GetComponent<IIntercatable>().Use(player);
    }

    public void getData(GameObject _player, GameObject _item){
        player=_player;
        item=_item;
    }
}
