using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    [SerializeField]
    private float jump;

    private Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {

        var horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRigidBody.velocity = new Vector2(horizontalInput * velocity, playerRigidBody.velocity.y);
        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //{
            //gameObject.transform.Translate(new Vector3(-1, 0, 0) * velocity * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    gameObject.transform.Translate(new Vector3(1, 0, 0) * velocity * Time.deltaTime);
        //}
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetButton("Jump"))
        {
            gameObject.transform.Translate(new Vector3(0, jump, 0) * velocity * Time.deltaTime);
        }
    }
}
