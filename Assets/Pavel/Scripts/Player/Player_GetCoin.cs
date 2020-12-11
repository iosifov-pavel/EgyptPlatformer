using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_GetCoin : MonoBehaviour
{
    //[SerializeField] private GameObject LvlC;
    GameObject UI;
    GameObject score;
    Text score_text;
    int coins=0;
    // Start is called before the first frame update
    private void Start() {
        UI = transform.parent.gameObject.GetComponent<Player_InfoHolder>().getUI();
        score = UI.transform.GetChild(1).GetChild(4).GetChild(0).gameObject;
        score_text = score.GetComponent<Text>();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.tag == "Coin"){
            Destroy(collision.gameObject);
            //LvlC.GetComponent<Level_Controller>().GetCoin(coinValue);
            coins++;
            score_text.text = coins.ToString();
        }
     }
}
