using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class Button_Jump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
public bool buttonPressed = false;
[SerializeField] private GameObject player;
Player_Movement pm;

private void Start() {
    pm = player.GetComponent<Player_Movement>();
}

private void Update() {
    if(Input.GetKeyDown(KeyCode.Space)) Pdown();
    else if(Input.GetKeyUp(KeyCode.Space)) PUp();
}

void Pdown(){
    if(!buttonPressed){
    buttonPressed = true;
    StartCoroutine(delay());
    Debug.Log("J pressed");
    }
}

void PUp(){
    buttonPressed = false;
    pm.buttonJump = false;
    pm.cant_jump = false;
    pm.jumps++;
    Debug.Log("J released");
}
 
public void OnPointerDown(PointerEventData eventData){
    Pdown();
}
 
public void OnPointerUp(PointerEventData eventData){
    PUp();
}

IEnumerator delay(){
    pm.buttonJump = true;
    yield return new WaitForSeconds(pm.jump_time_max);
    pm.buttonJump = false;
}
}