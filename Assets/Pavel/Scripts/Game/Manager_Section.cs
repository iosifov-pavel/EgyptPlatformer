using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Section : MonoBehaviour
{
    // Start is called before the first frame update
    Section section;
    [SerializeField] int id;
    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SectionManager");
        if (objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);    
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        section = new Section(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Section{
    public List<Level> section;
    string name;
    int section_id;
    bool complete;
    public Section(int id){
        section_id = id;
        name="sec_def";
        section = new List<Level>();
        complete = false;
    }
}
