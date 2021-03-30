using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleEyes : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    bool groundOnTheWay = false;
    [SerializeField] bool dontCareAboutGround = false;
    [SerializeField] LayerMask ground;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player = other.transform;
            CastRay();   
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player = other.transform;
            CastRay();   
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player = null;
        }
    }

    public Transform GetPlayer(){
        if(dontCareAboutGround) return player;
        else{
            if(groundOnTheWay) return null;
            else return player;
        }
    }


    void CastRay(){
        Vector3 pos= transform.position;
        Vector3 dir = player.position - transform.position;
        dir.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(pos,dir,10,ground);
        Debug.DrawRay(pos,dir*10,Color.red,0.01f);
        if(hit.collider!=null) groundOnTheWay = true;
        else groundOnTheWay = false;
    }
}
