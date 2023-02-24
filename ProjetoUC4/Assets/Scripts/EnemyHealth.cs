using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Vector2 enemy;
    private Transform playerPosition;

    public int life, maxLife = 100;
    public int speed;

    int amount;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
    }

    public void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyFollow();
       
    }

    public void EnemyFollow()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0) 
        {
            Destroy(gameObject);
        }
    }

}
