using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Range_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    Enemy_Ray_Eyes eyes;
    Enemy_Ground_Patroling egp;
    Transform player;
    float distance;
    float near = 0.5f;
    bool canAttcak;
    float far = 1;
    Animator animator;
    float time =1;
    bool stop=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
