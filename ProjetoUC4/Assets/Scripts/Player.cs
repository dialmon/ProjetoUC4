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
    private float jumpHeight, jumpForce = 8f;

    private Rigidbody2D playerRigidBody;

    private PlayerInput playerInput;
    private Vector2 horizontalInput;

    private GroundCheck groundCheck;

    SpriteRenderer playerFlip;
    private Animator pAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();

        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();

        groundCheck = gameObject.GetComponentInChildren<GroundCheck>();

        playerFlip = GetComponent<SpriteRenderer>();

        pAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInput.actions.Enable();
    }

    private void OnDisable()
    {
        playerInput.actions.Disable();
    }

    // Update is called once per frame
    public void Update()
    {
        playerRigidBody.velocity = new Vector2(horizontalInput.x * velocity, playerRigidBody.velocity.y);

        if (horizontalInput.x > 0)
        {
            playerFlip.flipX = false;
        }
        else if (horizontalInput.x < 0)
        {
            playerFlip.flipX = true;
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>();

        if (horizontalInput.x == 0)
        {
            pAnimator.Play("PlayerIdle");
        }
        else if (horizontalInput.x != 0)
        {
            pAnimator.Play("PlayerRun");
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && groundCheck.grounded)
        {
            //float newY = transform.position.y + jumpHeight;
            //transform.Translate(new Vector3(0, newY, 0) * Time.deltaTime);

            playerRigidBody.velocity = Vector2.up * jumpForce;

            pAnimator.Play("PlayerJump");
        }
        else if (context.phase == InputActionPhase.Started && !groundCheck.grounded && playerRigidBody.velocity.y > 0)
        {
            playerRigidBody.velocity = Vector2.up * jumpForce;

            pAnimator.Play("PlayerJump");
        }
        else if (context.phase == InputActionPhase.Canceled && playerRigidBody.velocity.y > 0)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y * 0.5f);

            pAnimator.Play("PlayerFall");
        }
    }
}
