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

    SpriteRenderer playerFlip;

    private Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerFlip = GetComponent<SpriteRenderer>();

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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRigidBody.velocity = new Vector2(horizontalInput * velocity, playerRigidBody.velocity.y);

        /*if (horizontalInput > 0)
        {
            transform.localScale = new Vector3 (5f, 5f, 5f);
        }

        else if (horizontalInput < 0)
        {
           transform.localScale = new Vector3 (-5f, 5f, 5f);
        }*/

        if (horizontalInput > 0)
        {
           playerFlip.flipX = false;
        }

        else if (horizontalInput < 0)
        {
            playerFlip.flipX = true;
        }
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
