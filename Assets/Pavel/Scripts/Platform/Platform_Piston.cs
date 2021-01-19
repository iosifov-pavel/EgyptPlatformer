using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Piston : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float time = 0.5f;
    [SerializeField] float speed = 1;
    [SerializeField] float height = 1;
    [SerializeField] bool up=false;
    Vector3 original, isup;
    float timer=0;
    float step;
    Transform leg;
    void Start()
    {
        leg = transform.GetChild(0);
        if(up){
            original = transform.position - leg.transform.up*height;
            isup = transform.position;
        }
        else{
            original = transform.position;
            isup = transform.position + leg.transform.up*height;
        }
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;
        if(!up){
            transform.position = Vector3.MoveTowards(transform.position,isup,step);
            if(transform.position==isup){
                StartCoroutine(ChangeState());
            }
        }
        if(up){
            transform.position = Vector3.MoveTowards(transform.position,original,step);
            if(transform.position==original){
                StartCoroutine(ChangeState());
            }
        }
    }

    IEnumerator ChangeState(){
        yield return new WaitForSeconds(time);
        if(up){
            up=false;
        }
        else if(!up){
            up=true;
        }
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
    if(other.gameObject.tag=="GroundCheck"){
            other.gameObject.transform.parent.SetParent(transform);
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            other.gameObject.transform.parent.SetParent(transform);
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="GroundCheck"){
            other.gameObject.transform.parent.SetParent(null);
        }
    }
}
