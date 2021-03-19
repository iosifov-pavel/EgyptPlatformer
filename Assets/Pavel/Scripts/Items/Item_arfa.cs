using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_arfa: MonoBehaviour,IIntercatable
{
    
    public GameObject enemy,arpha;

    Vector2 Pos1, Pos2;
    // Start is called before the first frame update
    void Start()
    {
        Pos1=enemy.transform.position;
        Pos2=arpha.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator kArfa (GameObject enemy)
    {   
        
        enemy.transform.position = Pos2;
        yield return new WaitForSeconds (2f);
        enemy.transform.position = Pos1;
    }

    
    public void Use(GameObject _player){
       if(_player.gameObject.CompareTag("Player"))
        {
            StartCoroutine(kArfa(enemy));
        }
    }
}
