using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicThrowCannon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject patron;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            GameObject new_patron = Instantiate(patron);
            new_patron.transform.position = transform.position;
            new_patron.SetActive(true);
            float v = HardLines.GetForce(new_patron.transform,other.transform, 45f);
            float dir = 1 * Mathf.Sign(other.transform.position.x-transform.position.x);
            var rb = new_patron.GetComponent<Rigidbody2D>();
            Vector2 move = new Vector2(dir,1);
            move.Normalize();
            rb.AddForce(move*v, ForceMode2D.Impulse);
        }
    }
}
