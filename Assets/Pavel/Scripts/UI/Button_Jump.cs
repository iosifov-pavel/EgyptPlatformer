using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class Button_Jump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public bool buttonPressed = false;
    [SerializeField] Movement player2;
    
    private void Start() {
    }
    
    private void Update() {
        if(Input.GetButtonDown("Jump")) Pdown();
        else if(Input.GetButtonUp("Jump")) PUp();
    }
    
    
    
    void PUp(){
        buttonPressed = false;
        player2.setJumpButton(false);
        player2.Jumping();
    }

    void Pdown(){
        if(buttonPressed || player2.GetJumpCount()>=2){
            return;
        }
        buttonPressed = true;
        //player2.Jumping();
        player2.setJumpButton(true);

    }


    public void OnPointerDown(PointerEventData eventData){
        Pdown();
    }
    public void OnPointerUp(PointerEventData eventData){
        PUp();
    }
}