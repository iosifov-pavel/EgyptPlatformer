using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game_Manager
{
    public static int lives=3;



    public static void PlayerDead(){
        lives--;
    }

    public static void SetLives(){

    }

    public static void AddLives(){
        lives++;
    }
}
