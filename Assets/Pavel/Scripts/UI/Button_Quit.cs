﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Quit : MonoBehaviour
{
    public void RageQuit()
    {
        Application.Quit();
        Debug.Log("Game left");
    }
}
