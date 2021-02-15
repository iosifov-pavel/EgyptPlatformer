using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    public GameObject PanelMenu;
    public GameObject SettingsMenu;
    public GameObject SoundBt;
    public GameObject LanguageBt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingsBtm()
    {
        Time.timeScale = 0f;
        PanelMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        
    }

    public void SoundBtm()
    {
        Time.timeScale = 0f;
        LanguageBt.SetActive(false);
        SoundBt.SetActive(true);
    }

    public void LanguageBtm()
    {
        Time.timeScale = 0f;
        SoundBt.SetActive(false);
        LanguageBt.SetActive(true);
    }

    public void BackBtm()
    {
        Time.timeScale = 0f;
        SettingsMenu.SetActive(false);
        PanelMenu.SetActive(true);
       
    }
}
