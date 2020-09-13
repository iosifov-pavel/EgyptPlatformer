using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    int dmg = 2;
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
        
       /* Rigidbody2D plb = other.gameObject.GetComponent<Rigidbody2D>();
        CharacterControl cc = other.gameObject.GetComponent<CharacterControl>();
        
        plb.AddForce(new Vector3(-1,2,0)*3,ForceMode2D.Impulse);*/
        StartCoroutine(playerGetHit(other));
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        player.Hurt(dmg);
        
        
    }

    
    private IEnumerator playerGetHit(Collider2D player) 
    {

        yield return new WaitForSeconds(0.3f);
        
    }
  
}
