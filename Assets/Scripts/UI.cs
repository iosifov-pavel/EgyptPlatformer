using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LevelControl level;
    [SerializeField] Text score;
    void Start()
    {
       GetScore("0");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetScore(string s) {
        score.text= s;
    }
}
