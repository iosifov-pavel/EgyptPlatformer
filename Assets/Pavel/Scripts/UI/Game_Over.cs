using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    // Start is called before the first frame update
    Game_Preload manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameIsOver(){
        //manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Preload>();
        //manager.Loading();
        SceneManager.LoadScene("Map");
    }
}
