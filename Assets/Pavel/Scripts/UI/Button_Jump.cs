using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class Button_Jump : MonoBehaviour
, IPointerDownHandler, IPointerUpHandler 
{
 
public bool buttonPressed = false;
[SerializeField] private GameObject player;
[SerializeField] Movement player2;
Player_Movement pm;

private void Start() {
    pm = player.GetComponent<Player_Movement>();
}

private void Update() {
    //if(pm.jump_block) return;
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
    pm.buttonJump = false;
    buttonPressed = false;
    pm.cant_jump = false;
    player2.setJumpButton(false);
}

void Pdown(){
    if(pm.buttonJump || buttonPressed){
        return;
    }
    buttonPressed = true;
    pm.buttonJump = true;
    player2.setJumpButton(true);
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