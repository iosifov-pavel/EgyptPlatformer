using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class Button_Jump : MonoBehaviour
, IPointerDownHandler, IPointerUpHandler 
{
 
public bool buttonPressed = false;
[SerializeField] private GameObject player;
Player_Movement pm;

private void Start() {
    pm = player.GetComponent<Player_Movement>();
}

private void Update() {
    if(Input.GetButtonDown("Jump")) Pdown();
    else if(Input.GetButtonUp("Jump")) PUp();
    //else if(Input.GetKeyUp(KeyCode.Space)) PUp();
}

//void Pdown(){
//    if(!buttonPressed){
//    buttonPressed = true;
//    StartCoroutine(delay());
//    Debug.Log("J pressed");
//    }
//}
//void PUp(){
//    buttonPressed = false;
//    pm.buttonJump = false;
//    pm.cant_jump = false;
//    pm.jumps++;
//    Debug.Log("J released");
//}
//public void OnClick(){
//    //if(!buttonPressed){
//    //buttonPressed = true;
//    pm.buttonJump = true;
//    pm.jumps++;
//    Debug.Log("Jump2");
//    //}
//}

void PUp(){
    buttonPressed = false;
}

void Pdown(){
    if(buttonPressed){
        return;
    }
    buttonPressed = true;
    pm.buttonJump = true;
    //pm.jumps++;
}
 
//public void OnPointerDown(PointerEventData eventData){
//    //Pdown();
//    Pdown2();
//}
 
//public void OnPointerUp(PointerEventData eventData){
//    PUp();
//}

public void OnPointerDown(PointerEventData eventData){
    Pdown();
    //Pdown2();
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