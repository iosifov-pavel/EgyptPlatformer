using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active = false;
    [SerializeField] int id;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(active) return;
        if(other.gameObject.tag=="Player" && other.gameObject.layer==9 || other.gameObject.layer==10){
            active=true;
            other.gameObject.GetComponent<Player_Health>().SetCheckPoint(transform,id);
        }
    }
}
