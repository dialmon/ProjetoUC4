using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    [SerializeField]
    private float jumpHeight;

    private float jumpForce;

    private float normalGravity;
    private float fallGravity = 4;

    private Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();

        normalGravity = playerRigidBody.gravityScale;
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * normalGravity));
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
    }

    void Jump()
    {

        if (Input.GetButton("Jump"))
            playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        if (playerRigidBody.velocity.y >= 0)
        {
            playerRigidBody.gravityScale = normalGravity;
        }
        else if (playerRigidBody.velocity.y < 0) // Caindo
        {
            playerRigidBody.gravityScale = fallGravity;
        }

    }
}
