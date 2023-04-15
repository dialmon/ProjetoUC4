using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public int enemyDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D damageTake)
    {
        if (damageTake.gameObject.TryGetComponent<PlayerLife>(out PlayerLife giveDamage))
        {
            Debug.Log($"take damage");
            giveDamage.PlayerTakeDamage(enemyDamage);
        }
    }
}
