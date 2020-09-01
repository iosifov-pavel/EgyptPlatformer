using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float smoth = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        transform.position = player.transform.position+offset;
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
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position+offset, ref velocity, smoth);
    }
}
