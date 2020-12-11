using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenScene(int index)
    {
        Time.timeScale = 1f;
        //Game_Manager.lives=3;
        SceneManager.LoadScene(index);
    }
}
