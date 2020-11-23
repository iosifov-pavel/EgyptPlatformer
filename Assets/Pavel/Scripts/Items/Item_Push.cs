using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Push : MonoBehaviour, IIntercatable
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

    public void Use(GameObject _player){
        player=_player;
    }
}
