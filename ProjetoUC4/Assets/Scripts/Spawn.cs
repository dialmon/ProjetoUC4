using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public float time;
    public GameObject[] clients;
    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (stop)
        {
            time = Random.Range(1, 5);
            yield return new WaitForSeconds(time);
            Instantiate(clients[Random.Range(0, clients.Length)], transform.position, Quaternion.identity);
        }
    }
}
