using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerPosition;
    public float velocity;


    void Start()
    {
        
    }

    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.transform.position, velocity * Time.deltaTime);
    }
}
