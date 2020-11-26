using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GetCoin : MonoBehaviour
{
    [SerializeField] private GameObject LvlC;

    int coinValue = 1;
    // Start is called before the first frame update


    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.tag == "Coin"){
            Destroy(collision.gameObject);
            LvlC.GetComponent<Level_Controller>().GetCoin(coinValue);
        }
     }
}
