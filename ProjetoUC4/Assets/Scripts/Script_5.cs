using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Script_5 : MonoBehaviour
{
    public static Script_5 instance;

    public int damage;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rigby;
    public float velociity;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigby = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rigby.velocity = new Vector2(direction.x, direction.y).normalized * velociity;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }

    public void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Destroy(gameObject, 4);
    }

    public void Damage()
    {
        damage += 10;  
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Destroy(this.gameObject);
        }
    }

}
