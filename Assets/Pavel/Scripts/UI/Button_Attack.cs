using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class Button_Attack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
public bool buttonPressed = false;
[SerializeField] private GameObject player;
Player_Attack pa;

private void Start() {
    pa = player.GetComponent<Player_Attack>();
}
 
public void OnPointerDown(PointerEventData eventData){
     if(!buttonPressed){
     buttonPressed = true;
     pa.buttonAttack = true;
     Debug.Log("A pressed");
     }
}
 
public void OnPointerUp(PointerEventData eventData){
    buttonPressed = false;
    pa.buttonAttack = false;
    Debug.Log("A released");
}
}