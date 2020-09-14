using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHurtYou : MonoBehaviour
{
    int dmg = 1;
    
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
    private void OnCollisionEnter2D(Collision2D other) 
    {  
        StartCoroutine(playerGetHit(other));
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        player.Hurt(dmg);
    }

    
    private IEnumerator playerGetHit(Collision2D player) 
    {
        yield return new WaitForSeconds(1.5f);
    }
  
}
