using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform destination;
    //[SerializeField] public Vector2 where = Vector2.zero;
    //[SerializeField] public float power=14;
    //Portals dest_p;
    CircleCollider2D dest_c, this_c;

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,destination.position);
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, (Vector2)transform.position + where);
    }
    void Start()
    {
        this_c = GetComponent<CircleCollider2D>();
        dest_c = destination.gameObject.GetComponent<CircleCollider2D>();
        //dest_p = destination.GetComponent<Portals>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(delay());
            other.transform.position = destination.position;
        }
    }

    IEnumerator delay(){
        dest_c.enabled = false;
        this_c.enabled = false;
        yield return new WaitForSeconds(2f);
        dest_c.enabled = true;
        this_c.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
    }
}
