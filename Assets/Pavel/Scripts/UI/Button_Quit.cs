using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Quit : MonoBehaviour
{
    Manager_Section manager_Section;
    public void RageQuit()
    {
        manager_Section = GameObject.FindGameObjectWithTag("SectionManager").GetComponent<Manager_Section>();
        string name = string.Format("S{0}",manager_Section.section.section_id);
        //Application.Quit();
        Debug.Log("Back to Map");
        SceneManager.LoadScene(name);
        
    }
}
