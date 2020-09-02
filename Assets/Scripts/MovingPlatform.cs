using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D body;
     public CharacterControl player;
    private Vector3 start;
    public Vector3 end =Vector3.zero;
    public float pspeed = 1f;
    private float percent=0;
    int forward = 1;
    void Start()
    {
        start = transform.position;
        body = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, end);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name =="Player")
        {
            collision.collider.transform.SetParent(transform);
            player.other_source = pspeed*10;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        percent +=forward*pspeed*Time.deltaTime;
        float x = (end.x-start.x)*percent + start.x;
        float y = (end.y - start.y) * percent + start.y;
        transform.position = new Vector3(x,y,start.z);

        if(forward==1&& percent>= 0.9f||forward==-1&&percent<=0.1f)
        {
            forward*=-1;
        }
    }
}
