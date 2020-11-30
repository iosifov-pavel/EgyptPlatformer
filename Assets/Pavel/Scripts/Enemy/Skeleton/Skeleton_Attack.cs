using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    //LayerMask player = LayerMask.GetMask("Player");
    Enemy_Ray_Eyes eyes;
    int dir;
    Enemy_Ground_Patroling egp;
    Transform player;
    float distance;
    float near = 0.5f;
    bool canAttcak;
    float far = 2f;
    Animator animator;
    float time = 1.5f;
    public bool stop=false;
    void Start()
    {
        eyes = GetComponent<Enemy_Ray_Eyes>();
        egp = GetComponent<Enemy_Ground_Patroling>();
        distance = 666;
        animator = GetComponent<Animator>();
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
                if(!stop){
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
}
