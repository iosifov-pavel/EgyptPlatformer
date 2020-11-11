using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] private GameObject button;
   // public bool interact = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if(!interact) return;

    }

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
