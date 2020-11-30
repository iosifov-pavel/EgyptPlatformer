using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    //LayerMask player = LayerMask.GetMask("Player");
    Enemy_Ray_Eyes eyes;
    Skeleton_Block skeleton_Block;
    int dir;
    Enemy_Ground_Patroling egp;
    Transform player;
    float distance;
    float near = 0.5f;
    bool canAttcak;
    public int canB=0;
    float far = 2f;
    float far_far = 4;
    Animator animator;
    float time = 1.5f;
    public bool stop=false;
    void Start()
    {
        eyes = GetComponent<Enemy_Ray_Eyes>();
        egp = GetComponent<Enemy_Ground_Patroling>();
        distance = 666;
        animator = GetComponent<Animator>();

        if(TryGetComponent(out Skeleton_Block skeleton_Block)){
            canB = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x)*1;
        if(eyes.Check()!=null){
            player = eyes.Check();
            distance=Mathf.Abs(transform.position.x-player.position.x);
        }
        else distance=666;
        if(distance==666) return;
        if(distance<=far){
            if(distance<=near){
                if(!stop && canB!=2){
                StopAllCoroutines();
                StartCoroutine(stopps());
                egp.StopIt(time);
                }
            }
        }
    }

    IEnumerator stopps() {
        stop=true;
        canAttcak=true;
        animator.SetBool("Attack",canAttcak);
        yield return new WaitForSeconds(time);
        stop=false;
        canAttcak=false;
        animator.SetBool("Attack",canAttcak);
    }

    public void Interupt(){
        StopAllCoroutines();
    }
}
