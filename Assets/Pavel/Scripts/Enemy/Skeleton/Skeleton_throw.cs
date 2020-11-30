using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_throw : MonoBehaviour
{
    Enemy_Ray_Eyes eyes;
    Skeleton_Block skeleton_Block;
    int count = 0;
    int dir;
    float speed = 4;
    [SerializeField] GameObject body;
    GameObject spear_copy;
    [SerializeField] GameObject spear2;
    Enemy_Ground_Patroling egp;
    Transform player;
    float distance;
    bool canThrow;
    public int canB=0;
    float far = 2f;
    float far_far = 4f;
    Animator animator;
    float time = 1.1f;
    public bool has_copy=false;
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
            //StopAllCoroutines();
        }
        if(distance>far && distance<=far_far){
            if(!has_copy && canB!=2){
                StopAllCoroutines();
                StartCoroutine(throws());
                count++;
                egp.StopIt(time);
            } 
        }
    }

     public void Throw(){
        spear_copy = Instantiate(spear2) as GameObject;
        spear_copy.transform.parent = body.transform;
        spear_copy.transform.localPosition = spear2.transform.localPosition;
        spear_copy.transform.localRotation = spear2.transform.localRotation;
        spear_copy.transform.localScale = spear2.transform.localScale;
        Fly();
        
    }

    public void Fly(){
        spear_copy.GetComponent<Rigidbody2D>().velocity = Vector2.right * dir * speed;
        spear_copy.transform.parent = null;
    }

    IEnumerator throws(){
        canThrow = true;
        has_copy= true;
        animator.SetBool("Throw",canThrow);
        yield return new WaitForSeconds(time);
        canThrow = false;
        animator.SetBool("Throw",canThrow);
        if(count==2) {
            yield return new WaitForSeconds(0.3f);
            has_copy=false;
            count=0;
        }
        else has_copy = false;
    }

    public void Interupt(){
        StopAllCoroutines();
    }

}
