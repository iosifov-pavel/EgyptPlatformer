using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropSpwn : MonoBehaviour
{
    public GameObject DropWater;
    public float cooldown = 0;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            cooldown -= Time.deltaTime;
            while (cooldown < 0)
            {
                cooldown += 0.01f;
                Instantiate(DropWater, position:(Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition)+Random.insideUnitCircle*.2f, Quaternion.identity);
            }
        }
    }
}
