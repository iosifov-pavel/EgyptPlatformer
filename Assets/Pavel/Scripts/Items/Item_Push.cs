using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Push : MonoBehaviour, IIntercatable
{
    // Start is called before the first frame update
    GameObject player;
    Player_Movement pm;
    float speed = 0.5f;
    bool on = false;
    Rigidbody2D rb2;
    Rigidbody2D  playerrb;
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        rb2.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(on){
            
            
            Vector2 step = new Vector3(pm.direction.x * speed * Time.deltaTime,0);
            //transform.Translate(step);
            rb2.MovePosition((Vector2)rb2.position+step);
            playerrb.MovePosition((Vector2)playerrb.position+step);
            //rb2=playerrb;
            //rb2.velocity=step;
        }
    }

    public void Use(GameObject _player){
        on = on==true ? false : true;
        if(on){
            rb2.isKinematic=false;
            player=_player;
            pm=player.GetComponent<Player_Movement>();
            playerrb = player.GetComponent<Rigidbody2D>();
            pm.blocked = true;
            player.transform.parent = transform;
        } else{
            rb2.isKinematic=true;
            pm.blocked = false;
            player.transform.parent=null;
            player=null;
            pm=null;
        }
    }
}
