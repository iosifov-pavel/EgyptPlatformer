using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Controller : MonoBehaviour
{
    [SerializeField] private Text score;

    int coins = 0;

    public Image [] hearts;
    public Sprite isLife, nonLife;

    Player_Health player;

    public GameObject WinScreen;
    public GameObject lul;

    public GameObject DeathScreen;
    

    
    // Start is called before the first frame update
    void Start()
    {
        score.text = coins.ToString();
    }

    // Update is called once per frame
   void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            //if(player.GetHP() > i)
            //    hearts[i].sprite = isLife;
            //else 
            //    hearts[i].sprite = nonLife;
        }
    }


    public void GetCoin(int i)
    {   
        coins += i;
        score.text = coins.ToString();
    }

    public void Win ()
    {
        Time.timeScale = 0f;
        WinScreen.SetActive(true);
    }


    public void Lose ()
    {
        Time.timeScale = 0f;
        DeathScreen.SetActive(true);
    }
    
}
