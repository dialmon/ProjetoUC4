using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerLife : MonoBehaviour
{
    public Canvas rToRespawn;
    public int playerLife, playerMaxLife = 100;

    public string sceneLoad;


    // Start is called before the first frame update
    void Start()
    {
        playerLife = playerMaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        TextOff();
    }
    public void PlayerTakeDamage (int takingDamage)
    {
        playerLife -= takingDamage;
        
        if (playerLife <= 0)
        {
            TextOn();
            Destroy(gameObject);
        } 
    }

    public void TextOn()
    {
        rToRespawn.gameObject.SetActive(true);
    }
    public void TextOff()
    {
        rToRespawn.gameObject.SetActive(false);
    }
}
