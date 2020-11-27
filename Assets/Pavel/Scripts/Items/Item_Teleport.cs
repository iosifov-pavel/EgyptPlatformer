using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Teleport : MonoBehaviour,IIntercatable
{
     public GameObject Door;
     public GameObject Player;

     bool condition = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   // private void UseTeleport(Collider2D other) {
   //     
   //         // Teleport(other.gameObject);
   //     
   //     
   // }

    IEnumerator Teleport ()
    {   
       // Player_Movement plar  = Player.GetComponent<Player_Movement>(); 
        yield return new WaitForSeconds (1);
        Player.transform.position = new Vector2(Door.transform.position.x, Door.transform.position.y);
    }

    
    public void Use(GameObject Player){
       // player=Player;
       // condition = condition==false ? true : false;
       if(Player.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Teleport());
        }
    }
}
