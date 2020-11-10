using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Restart : MonoBehaviour
{
  public void RestartGame ()
  {
    SceneManager.LoadScene("Test");
  }
}
