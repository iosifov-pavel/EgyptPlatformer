using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer web;
    Vector2 ray,ray2;
    float speed_up=4f;
    float speed_down=2f;
    float time = 0.5f;
    RaycastHit2D hit,hit2;
    Vector2 original;
    LayerMask player;
    LayerMask player2;
    LayerMask p;
    bool down=false,up=false;
    void Start()
    {
        ray=Vector2.down;
        ray2=Vector2.down;
        player = LayerMask.GetMask("Player");
        player2 = LayerMask.GetMask("Damaged");
        p = player | player2;
        original=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Spider_Sense();
        if(down) Jump();
        if(up) Back();   
    }

    void Spider_Sense(){
        Vector2 pos,pos2;
        pos = (Vector2)transform.position + new Vector2(0.1f,0);
        pos2 = (Vector2)transform.position - new Vector2(0.1f,0);

        hit = Physics2D.Raycast(pos,ray,4f,p);
        hit2 = Physics2D.Raycast(pos2,ray2,4f,p);

        if(hit.collider!=null || hit2.collider!=null){
            down=true;
        }
    }

    void Jump(){
        float step = speed_down*Time.deltaTime;

    }

    void Back(){

    }

    private void OnCollisionEnter2D(Collision2D other) {
        float a=1;
    }
}
