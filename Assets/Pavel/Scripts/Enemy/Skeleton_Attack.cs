using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    //LayerMask player = LayerMask.GetMask("Player");
    Enemy_Ray_Eyes eyes;
    int dir;
    float speed = 4;
    [SerializeField] GameObject body;
    [SerializeField] GameObject hand;
    GameObject spear_copy;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject spear2;
    [SerializeField] GameObject sword2;
    Enemy_Ground_Patroling egp;
    Transform player;
    float distance;
    float near = 0.5f;
    bool canAttcak;
    bool canThrow;
    float far = 1.5f;
    float far_far = 4;
    Animator animator;
    float time =1;
    bool stop=false;
    bool has_copy=false;
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
                StartCoroutine(stopps());
                egp.StopIt(time);
                }
            }
        }
        if(distance>far && distance<=far_far){
            Grab();
        }
    }

    IEnumerator stopps() {
        sword2.SetActive(false);
        sword.SetActive(true);
        stop=true;
        canAttcak=true;
        animator.SetBool("Attack",canAttcak);
        yield return new WaitForSeconds(time);
        stop=false;
        canAttcak=false;
        animator.SetBool("Attack",canAttcak);
        sword2.SetActive(true);
        sword.SetActive(false);
    }

    void Grab(){
        if(!has_copy){
            StartCoroutine(throws());
            egp.StopIt(time);
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
        has_copy = false;
        animator.SetBool("Throw",canThrow);
    }
}
