using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_NextLvl : MonoBehaviour
{
    Manager_Section manager_Section;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Nexturu()
    {
        Time.timeScale = 1f;
        manager_Section = GameObject.FindGameObjectWithTag("SectionManager").GetComponent<Manager_Section>();
        string name = string.Format("S{0}",manager_Section.section.section_id);
        SceneManager.LoadScene(name);
    }

}
