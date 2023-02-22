using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth: MonoBehaviour
{

    public Vector2 enemy;
    private Transform playerPosition;

    public int life;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyFollow();
        Healty();
    }

    public void EnemyFollow()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.transform.position, speed * Time.deltaTime);
    }

    public void Healty()
    {
        if (life < 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullets")
        {
            Script_5.instance.Damage();
            life -= 10;
        }
    }

}
