using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_GetCoin : MonoBehaviour
{
    //[SerializeField] private GameObject LvlC;
    [SerializeField] Manager_Level LM;
    GameObject UI;
    GameObject score;
    Text score_text;
    public int coins=0;
    public int collected_coins=0;
    // Start is called before the first frame update
    private void Start() {
        collected_coins = LM.collected_coins;
        UI = GetComponent<Player_InfoHolder>().getUI();
        score = UI.transform.GetChild(1).GetChild(4).GetChild(0).gameObject;
        score_text = score.GetComponent<Text>();
        score_text.text = collected_coins.ToString();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.tag == "Coin"){
            int v = collision.gameObject.GetComponent<Item_Coins>().getValue();
            if(v==1) Player_Sounds.sounds.PlaySound("coin");
            else if(v==5) Player_Sounds.sounds.PlaySound("bigcoin");
            //collected_coins+=v;
            coins+=v;
            Manager_Level.GetCoin(v);
            collected_coins = LM.collected_coins;
            //score_text.text = collected_coins.ToString();
            Destroy(collision.gameObject);
        }
     }
}
