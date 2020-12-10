using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Level : MonoBehaviour
{
    [SerializeField] int level_id;
    Level info;

    // Start is called before the first frame update
    void Start()
    {
        info = new Level(level_id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Level{
    public int id;
    public bool complete;
    public float time;
    public int score;
    public string name;

    public Level(int i){
        id = i;
        complete = false;
        time = 0;
        score=0;
        name = "default";
    }
}
