using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    float lifetime = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime-=Time.deltaTime;
        if(lifetime<=0) Destroy(gameObject);
    }
}
