using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridge_sec : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    FallingBridge bridge;
    void Start()
    {
        bridge = transform.parent.gameObject.GetComponent<FallingBridge>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetId(int n){
        id=n;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            if(bridge.canSetContact){
                bridge.wasContact = true;
                bridge.setContactId(id);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        bridge.wasContact=false;
        bridge.setContactId(-1);
    }

}
