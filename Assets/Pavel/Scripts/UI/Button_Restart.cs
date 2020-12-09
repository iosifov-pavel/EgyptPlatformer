using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Restart : MonoBehaviour
{
  //[SerializeField] GameObject deathscreen, loosescreen; 
  public void RestartGame ()
  {
          //loosescreen.SetActive(false);
          //deathscreen.SetActive(false);
          Time.timeScale = 1f;
          Scene scene = SceneManager.GetActiveScene();
          SceneManager.LoadScene(scene.name);
          //level.PauseOff();
          
          
  }
}
