using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Restart : MonoBehaviour
{ 
  public Button_Pause level;
  public void RestartGame ()
  {
          
          Scene scene = SceneManager.GetActiveScene();
          SceneManager.LoadScene(scene.name);
          //level.PauseOff();
          
  }
}
