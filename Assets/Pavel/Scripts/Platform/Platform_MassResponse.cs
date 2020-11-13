using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_MassResponse : MonoBehaviour
{
    Transform parent;
    BoxCollider2D box;
    float size;
    float diff;
    float max_angle = 45;
    float percent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        box = parent.gameObject.GetComponent<BoxCollider2D>();
        size = box.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion angle = Quaternion.Euler(0,0,max_angle*percent/100);
        parent.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
           diff = parent.position.x - other.gameObject.transform.position.x;
           percent = diff/(size/2)*100; 
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        diff = parent.position.x - other.gameObject.transform.position.x;
        percent = diff/(size/2)*100; 
    }

    private void OnTriggerExit2D(Collider2D other) {
        diff=0;
        percent=0;
    }
}
