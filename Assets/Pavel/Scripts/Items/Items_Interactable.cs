using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_Interactable : MonoBehaviour
{
    [SerializeField] private GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            button.SetActive(false);
        }
    }
}
