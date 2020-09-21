using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D body;
    private Vector3 start;
    public Vector3 end = Vector3.zero;
    private float pspeed = 0.6f;
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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Collider2D hit = collision.gameObject.GetComponent<CharacterControl>().CheckBox();
            if(hit!=null && hit.gameObject.tag=="MovPlat")
            {
            collision.collider.transform.SetParent(transform);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            collision.collider.transform.SetParent(null);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        percent +=forward*pspeed*Time.fixedDeltaTime;
        float x = (end.x-start.x)*percent + start.x;
        float y = (end.y - start.y) * percent + start.y;
        transform.position = new Vector3(x,y,start.z);

        if(forward==1&& percent>= 0.95f||forward==-1&&percent<=0.05f)
        {
            forward*=-1;
        }
    }
}
