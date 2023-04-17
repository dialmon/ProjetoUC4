using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    public int playerLife, playerMaxLife = 100;

    //aleatorio


    // Start is called before the first frame update
    void Start()
    {
        playerLife = playerMaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerTakeDamage (int takingDamage)
    {
        playerLife -= takingDamage;
        if (playerLife <= 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Destroy(gameObject);
        }    
    }
}
