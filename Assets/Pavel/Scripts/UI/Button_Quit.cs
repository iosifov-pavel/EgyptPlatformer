using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Quit : MonoBehaviour
{
    Manager_Level manager_Level;
    public void RageQuit()
    {
        manager_Level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Manager_Level>();
        string name = string.Format("S{0}",manager_Level.section_id);
        //Application.Quit();
        Debug.Log("Back to Map");
        SceneManager.LoadScene(name);
        
    }
}
