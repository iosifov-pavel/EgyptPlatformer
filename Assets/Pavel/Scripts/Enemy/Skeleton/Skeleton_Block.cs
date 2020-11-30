using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Block : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject shield;
    Skeleton_Attack skeleton_Attack;
    Skeleton_throw skeleton_Throw;
    Enemy_Ground_Patroling enemy_Ground_Patroling;
    bool canA=false, canT=false;
    int durability = 2;
    bool isBlocking = false;
    bool block = false;
    BoxCollider2D shield_colider;
    RaycastHit2D hit2D;
    Animator body;
    int dir;
    float time = 0.5f;
    bool broken = false;
    void Start()
    {
        shield_colider = shield.GetComponent<BoxCollider2D>();
        body = GetComponent<Animator>(); 
        enemy_Ground_Patroling = GetComponent<Enemy_Ground_Patroling>();   
        if(TryGetComponent(out Skeleton_Attack s)){
            canA = true;
            skeleton_Attack = s;
        }
        if(TryGetComponent(out Skeleton_throw t)){
            canT = true;
            skeleton_Throw = t;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(broken) return;
        dir = (int)Mathf.Sign(transform.localScale.x)*1;
        hit2D = Physics2D.BoxCast(shield.transform.position,shield_colider.size,0,Vector2.right*dir,4f);
        if(hit2D.collider!=null && hit2D.collider.gameObject.tag=="Player_Bullet"){
            isBlocking=true;
        }
        if(isBlocking && !block){
            StartCoroutine(Block());
            durability--;
        }
        if(durability<=0){
            broken=true;
            if(canA){
                skeleton_Attack.canB=1;
            }
            if(canT){
                skeleton_Throw.canB=1;
            }
            shield.SetActive(false);
        }
    }

    IEnumerator Block(){
        block = true;
        isBlocking=false;
        if(canA){
            skeleton_Attack.canB=2;
        }
        if(canT){
            skeleton_Throw.canB=2;
        }
        enemy_Ground_Patroling.CanGo();
        body.SetBool("Block", block);
        yield return new WaitForSeconds(time);
        if(canA){
            skeleton_Attack.canB=1;
            skeleton_Attack.stop=false;
            }
        if(canT){
            skeleton_Throw.canB=1;
            skeleton_Throw.has_copy=false;
        }
        block=false;
        body.SetBool("Block", block);
    }
}
