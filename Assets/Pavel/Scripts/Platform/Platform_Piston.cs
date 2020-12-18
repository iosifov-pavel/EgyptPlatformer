using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Piston : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float time = 0.5f;
    [SerializeField] float speed = 1;
    [SerializeField] float height = 1;
    [SerializeField] bool up=false, down = true;
    Vector3 original, isup;
    float timer=0;
    float step;
    void Start()
    {
        step = speed * Time.deltaTime;
        if(up){
            original = transform.position - new Vector3(0,height,0);
            isup = transform.position;
        }
        else{
            original = transform.position;
            isup = transform.position + new Vector3(0,height,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(down){
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
            down=true;
        }
        else if(down){
            up=true;
            down=false;
        }
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
    if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(transform);
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(transform);
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(null);
        }
    }
}
