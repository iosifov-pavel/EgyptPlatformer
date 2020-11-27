using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void  PauseOff()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }
}
