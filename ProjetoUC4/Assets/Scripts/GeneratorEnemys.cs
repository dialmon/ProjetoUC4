using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEnemys : MonoBehaviour
{
    public GameObject[] SpawnEnemys;
    public Transform[] SpawnPoints;

    public float timerMaxSpawns;

    public float timerCurrentSpawns;
    void Start()
    {
        timerCurrentSpawns = timerMaxSpawns;
    }

    
    void Update()
    {
        timerCurrentSpawns -= Time.deltaTime;

        if(timerCurrentSpawns <= 0) 
        {
            SpawnObject();
        }
    }

    private void SpawnObject() 
    { 
        int objectRandom = Random.Range(0, SpawnEnemys.Length);
        int pointRandom = Random.Range(0, SpawnPoints.Length);

        Instantiate(SpawnEnemys[objectRandom], SpawnPoints[pointRandom].position, Quaternion.Euler(0f, 0f, 0f));
        timerCurrentSpawns = timerMaxSpawns;

        Debug.Log(timerCurrentSpawns);
    }

}
