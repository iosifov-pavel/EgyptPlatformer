using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_NextLvl : MonoBehaviour
{
    Manager_Level manager_Level;
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
        manager_Level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Manager_Level>();
        string name = string.Format("S{0}",manager_Level.section_id);
        SceneManager.LoadScene(name);
    }

}
