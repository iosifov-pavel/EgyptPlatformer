using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByPlayerDistance : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float distance = 10f;
    [SerializeField] GameObject target;
    bool active = false;
    Vector2 originalPos;
    Transform player;
    float dist;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target.SetActive(false);
        originalPos = target.transform.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(target.transform.position, player.position);
        if(dist<=distance){
            target.SetActive(true);
            active = true;
        } 
        if(active && dist>=distance*2){
            target.SetActive(false);
            active = false;
            target.transform.position = originalPos;
        }       
    }
}