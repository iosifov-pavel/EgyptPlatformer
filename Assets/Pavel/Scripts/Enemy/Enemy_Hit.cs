using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hit : MonoBehaviour
{
    // Start is called before the first frame update
    //LayerMask player = LayerMask.GetMask("Player");
    Enemy_Ray_Eyes eyes;
    Enemy_Ground_Patroling egp;
    Transform player;
    float distance;
    float near = 0.5f;
    bool canAttcak;
    float far = 1;
    Animator animator;
    float time =1;
    bool stop=false;
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
        
        if(eyes.Check()!=null){
            player = eyes.Check();
            distance=Mathf.Abs(transform.position.x-player.position.x);
        }
        else distance=666;
        if(distance==666) return;
        if(distance<=far){
            if(distance<=near){
                animator.SetBool("Attack",canAttcak);
                if(!stop){
                StartCoroutine(stopps());
                egp.StopIt(time);
                }
            }
        }
    }

    IEnumerator stopps() {
        stop=true;
        canAttcak=true;
        yield return new WaitForSeconds(time);
        stop=false;
        canAttcak=false;
    }
}
