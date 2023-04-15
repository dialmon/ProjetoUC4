using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    [SerializeField]
    private float jumpHeight;

    SpriteRenderer playerFlip;

    private Rigidbody2D playerRigidBody;

    private PlayerInput playerInput;
    private Vector2 horizontalInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerFlip = GetComponent<SpriteRenderer>();

        playerInput = gameObject.GetComponent<PlayerInput>();

        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    /*
    private void OnEnable()
    {
        playerInput.actions.Enable();
    }

    private void OnDisable()
    {
        playerInput.actions.Disable();
    }
    */

    // Update is called once per frame
    public void Update()
    {
        playerRigidBody.velocity = new Vector2(horizontalInput.x * velocity, playerRigidBody.velocity.y);


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

    public void OnMovement(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            float newY = transform.position.y + jumpHeight;

            transform.Translate(new Vector3(0, newY, 0) * Time.deltaTime);
        }
    }
}
