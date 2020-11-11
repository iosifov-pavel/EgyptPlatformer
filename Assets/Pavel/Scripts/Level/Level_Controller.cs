using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Controller : MonoBehaviour
{
    [SerializeField] private Text score;

    int coins = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
/*   void Update()
    {
        
    }*/


    public void Manager (int i)
    {   
        coins += i;
        score.text = coins.ToString();
    }
}
