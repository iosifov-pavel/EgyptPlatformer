using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Open_Level : MonoBehaviour
{
    [SerializeField] int level_to_load;
    GameObject ms;
    Manager_Section manager_Section;
    Level level;
    string level_name;
    GameObject statwindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(){
        ms = GameObject.FindGameObjectWithTag("SectionManager");
        manager_Section = ms.GetComponent<Manager_Section>();
        level = manager_Section.section.levels[level_to_load-1];
        level_name = string.Format("{0}L{1}",manager_Section.id,level.id);

        statwindow = transform.parent.Find("LStats").gameObject;
        statwindow.SetActive(true);
        GameObject close = statwindow.transform.GetChild(3).gameObject;
        GameObject stats = statwindow.transform.GetChild(1).gameObject;
        GameObject lvl_name = statwindow.transform.GetChild(0).gameObject;
        GameObject play = statwindow.transform.GetChild(2).gameObject;

        Button close_b = close.GetComponent<Button>();
        close_b.onClick.AddListener(()=>statwindow.SetActive(false)); 
        Button play_b = play.GetComponent<Button>();
        play_b.onClick.AddListener(() => SceneManager.LoadScene(level_name, LoadSceneMode.Single));
        Text lvl_name_t = lvl_name.GetComponent<Text>();
        lvl_name_t.text = level.name;
        Text stats_t = stats.GetComponent<Text>();
        stats_t.text = "AAA";

        //SceneManager.LoadScene(level_to_load, LoadSceneMode.Single);
    }


}
