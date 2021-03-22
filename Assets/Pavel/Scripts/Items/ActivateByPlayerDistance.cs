using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByPlayerDistance : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float distance = 10f;
    [SerializeField] GameObject target;
    Transform player;
    float dist;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(transform.position, player.position);
        if(dist<=distance) target.SetActive(true);
    }
}
