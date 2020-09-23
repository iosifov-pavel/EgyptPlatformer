using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float smoth = 0.3f;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {  
        transform.position = new Vector3(player.position.x,player.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void LateUpdate()
    {
        if(player == null) return;
        Vector3 playerposition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, playerposition, ref velocity, smoth);
    }
}
