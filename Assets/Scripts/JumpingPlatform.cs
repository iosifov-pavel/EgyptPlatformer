using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x,0);
      //  rb.AddForce(Quaternion.Euler(0,0,this.transform.localRotation.z)*Vector3.up*8f, ForceMode2D.Impulse);
        rb.AddForce(transform.up*8f, ForceMode2D.Impulse);
        float a=1;
    }
}
