using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_Level : MonoBehaviour
{
    [SerializeField] string level_to_load;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(){
        SceneManager.LoadScene(level_to_load, LoadSceneMode.Single);
    }
}
