using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Movement : MonoBehaviour, IChild
{
    public bool On{get;set;} = false;
    public bool Done{get;set;} = true;
    Platform_Move platform_Move;
    // Start is called before the first frame update
    void Start()
    {
        platform_Move = GetComponent<Platform_Move>();
        platform_Move.enabled = On;
    }

    // Update is called once per frame
    void Update()
    {
       platform_Move.enabled = On;
    }
}
