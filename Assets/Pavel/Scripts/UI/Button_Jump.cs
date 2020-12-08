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
 
public void OnPointerDown(PointerEventData eventData){
     if(!buttonPressed){
     buttonPressed = true;
     StartCoroutine(delay());
     Debug.Log("J pressed");
     }
}
 
public void OnPointerUp(PointerEventData eventData){
    buttonPressed = false;
    pm.buttonJump = false;
    pm.cant_jump = false;
    pm.jumps++;
    Debug.Log("J released");
}

IEnumerator delay(){
    pm.buttonJump = true;
    yield return new WaitForSeconds(pm.jump_time_max);
    pm.buttonJump = false;
}
}