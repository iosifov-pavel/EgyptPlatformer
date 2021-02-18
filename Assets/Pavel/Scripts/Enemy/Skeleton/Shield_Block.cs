using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Block : MonoBehaviour
{
    // Start is called before the first frame update
    int durability = 2;
    public bool canBreak = true;
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReduceDurab(){
        if(!canBreak) return;
        durability--;
        if(durability<=0) gameObject.SetActive(false);
    }
}
