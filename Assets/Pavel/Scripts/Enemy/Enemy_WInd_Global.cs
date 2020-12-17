using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WInd_Global : MonoBehaviour
{   
    public  float STR;

    public Vector2 dir;

    Player_Movement pm;
    // Start is called before the first frame update
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float randomNumber = Random.Range(0f,10f);    
    }

 //   IEnumerator Doit ()
 //   {
 //       карутиной вызвать направление и сколько по времени и через сколько времени действие. Вызов надо сделать по определенному триггеру.
 //         а отображение направления ветра можно отобразить по анимации
 //   }
}
