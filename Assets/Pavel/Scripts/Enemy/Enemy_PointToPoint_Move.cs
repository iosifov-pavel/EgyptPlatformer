using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PointToPoint_Move : MonoBehaviour
{
    [SerializeField] private Vector3 p1;
    [SerializeField] private Vector3 p2;
    float speed = 1.7f;
    // Start is called before the first frame update

    private void OnDrawGizmos() {
        Gizmos.color=Color.red;
        Gizmos.DrawLine(p1,p2);
    }
    void Start()
    {
      //  p1 = transform.position;
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
}
