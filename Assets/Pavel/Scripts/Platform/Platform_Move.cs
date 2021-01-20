using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour
{
    //public Vector3 p1;
    [SerializeField] Vector3 move_point;
    private Vector3 original,point;
    private float speed = 1f;
    Transform platform;

    
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(original, point);
    }

    // Start is called before the first frame update
    void Start()
    {
        platform = transform.parent;
        original = platform.transform.position;
        point = original + move_point;
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime; 
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, point, step);
        if(platform.transform.position==point){
            Vector3 temp = original;
            original = point;
            point = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
    if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(platform);
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(platform);
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(null);
        }
    }
}

