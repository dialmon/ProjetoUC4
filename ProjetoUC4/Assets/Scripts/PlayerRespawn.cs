using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private bool buttomR;
    private PlayerLife life;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }
}
