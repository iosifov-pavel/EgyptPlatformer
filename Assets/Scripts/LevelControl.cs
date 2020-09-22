using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{

    [SerializeField] UI userin;
    
    public int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Coins: "+coins);
        if(Input.GetKey(KeyCode.R))
        {
            restartCurrentScene();
        }
    }

    public void restartCurrentScene(){
          Scene scene = SceneManager.GetActiveScene(); 
          SceneManager.LoadScene(scene.name);
     }

     public void GetCoin()
     {
         coins++;
         userin.GetScore(coins.ToString());
     }

     
}
