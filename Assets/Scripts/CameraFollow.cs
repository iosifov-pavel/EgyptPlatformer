using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    private float smoth = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.position.x,player.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        Vector3 playerposition = new Vector3(player.position.x, player.position.y, transform.position.z);
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, playerposition, ref velocity, smoth);
    }
}
