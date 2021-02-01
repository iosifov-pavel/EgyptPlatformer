using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject RockPrefab;
    [SerializeField] float maximumRocks = 4f;
    [SerializeField] float spawnPeriod = 2f;
    [SerializeField] bool infinitesRocks = true;
    int rockCount;
    bool canSpawn = true;
    Transform rockStack;
    // Start is called before the first frame update
    void Start()
    {
        rockStack = transform.GetChild(1); 
    }

    // Update is called once per frame
    void Update()
    {
        rockCount = rockStack.transform.childCount;
        if(!infinitesRocks){
            if(rockCount<maximumRocks && canSpawn){
                StartCoroutine(dropRock());
            }
        }
        else{
            if(canSpawn){
                StartCoroutine(dropRock());
            }
        }
    }

    IEnumerator dropRock(){
        GameObject rock = Instantiate(RockPrefab) as GameObject;
        rock.SetActive(true);
        rock.transform.parent = rockStack;
        rock.transform.localPosition = Vector3.zero;
        canSpawn = false;
        yield return new WaitForSeconds(spawnPeriod);
        canSpawn=true;
    }

}
