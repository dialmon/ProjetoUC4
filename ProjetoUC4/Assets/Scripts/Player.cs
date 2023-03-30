using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    private PlayerInput playerInput;
    private Vector2 horizontalInput;

    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;

        playerInput = gameObject.GetComponent<PlayerInput>();

        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();

        normalGravity = playerRigidBody.gravityScale;
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * normalGravity));
    }

    // Update is called once per frame
    public void Update()
    {
        playerRigidBody.velocity = new Vector2(horizontalInput.x * velocity, playerRigidBody.velocity.y);

        // grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);

        if (playerRigidBody.velocity.y < 0) // Caindo
        {
            grounded = true;
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (grounded)
        {
            // TODO - corrrigir pulo
            transform.Translate(new Vector3(0, jumpHeight, 0) * Time.deltaTime);

            grounded = false;
        }
    }
}
