using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour
{
    public Vector3 p1;
    public Vector3 p2;
    private float speed = 1f;

    
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(p1, p2);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position=p1;
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, p2, step);
        if(transform.position==p2){
            Vector3 temp = p1;
            p1 = p2;
            p2 = temp;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.transform.SetParent(null);
        }
    }
}

