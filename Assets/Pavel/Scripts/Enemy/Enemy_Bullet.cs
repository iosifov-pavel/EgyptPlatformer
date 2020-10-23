using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    private int damage = -1;
    private float speed = 3f;
    private float lifetime = 1.2f;
    Transform player;
    Vector3 pos;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player.position = new Vector3(player.position.x,player.position.y,transform.position.z);
        pos = player.position;
        dir = (pos-transform.position).normalized;
        float angle = Vector2.Angle(dir,Vector2.right);
        if(player.position.y<transform.position.y) angle*=-1;
        transform.Rotate(new Vector3(0,0,angle));
   }

    // Update is called once per frame
    void Update()
    {
        lifetime-=Time.deltaTime;
        if(lifetime<=0) Destroy(gameObject);
        if(player){
            float step =  speed * Time.deltaTime; 
            transform.Translate(Vector3.right*step);
        }
    }

    public void GetPlayerPos(Transform pos){
        player = pos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==9){
            other.gameObject.GetComponent<Player_Health>().ChangeHP(damage);
            Destroy(gameObject);
        }
    }
}
