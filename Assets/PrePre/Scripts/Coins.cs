using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    // Start is called before the first frame update
    LevelControl level;
    void Start()
    {
        level = GameObject.Find("Level").GetComponent<LevelControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name=="Player")
        {
            level.GetCoin();
            Destroy(this.gameObject);
        }
    }
}
