using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    //[SerializeField] private GameObject button;
    GameObject UI;
    GameObject button;
   // public bool interact = false;
    // Start is called before the first frame update
    private void Start() {
        UI = transform.parent.gameObject.GetComponent<Player_UIHolder>().getUI();
        button = UI.transform.GetChild(1).GetChild(3).gameObject;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Interactable"){
            button.SetActive(true);
            button.GetComponent<Button_Use>().getData(transform.parent.gameObject,other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Interactable"){
            button.SetActive(false);
        }
    }
}
