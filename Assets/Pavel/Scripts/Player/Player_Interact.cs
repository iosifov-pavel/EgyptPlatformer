using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    //[SerializeField] private GameObject button;
    GameObject UI;
    GameObject button;
    bool canInteract = false;
    public static Player_Interact player_Interact=null;
    public bool isInteracting = false;
   // public bool interact = false;
    // Start is called before the first frame update
    private void Start() {
        player_Interact = this;
        UI = transform.parent.gameObject.GetComponent<Player_InfoHolder>().getUI();
        button = UI.transform.GetChild(1).GetChild(3).gameObject;
    }

    private void Update() {
        //if(isInteracting) return;
        if(canInteract){
            Debug.Log("1");
            if(Input.GetKey(KeyCode.U) /*|| Input.GetMouseButtonDown(1)*/){
                Debug.Log("2");
                button.GetComponent<Button_Use>().Click();
            }
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) {
        if(isInteracting) return;
        if(other.gameObject.tag=="Interactable"){
            button.SetActive(true);
            canInteract=true;
            button.GetComponent<Button_Use>().getData(transform.parent.gameObject,other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(isInteracting) return;
        if(other.gameObject.tag=="Interactable"){
            button.SetActive(false);
            canInteract=false;
        }
    }
}
