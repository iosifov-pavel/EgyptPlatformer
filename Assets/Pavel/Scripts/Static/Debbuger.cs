using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debbuger : MonoBehaviour
{
    [SerializeField] private Text score;
    static Text text;

    private void Start() {
         text = score;
    }
    // Start is called before the first frame update
    public static void Print(string s){
        text.text=s;
    }
}
