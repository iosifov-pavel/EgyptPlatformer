using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental;

public class Debbuger : MonoBehaviour
{
    [SerializeField] UnityEngine.Experimental.Rendering.Universal.Light2D lightinh;
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
