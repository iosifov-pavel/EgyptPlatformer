using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Game : MonoBehaviour
{
    // Start is called before the first frame update
    Game game_info;

    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);    
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        game_info = new Game();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Game{
    public List<Section> game;

    public Game(){
        game = new List<Section>();
    }
}
