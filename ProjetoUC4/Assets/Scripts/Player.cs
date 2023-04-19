using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    // Velocidade da movimenta��o
    [SerializeField]
    private float velocity;

    // Altura e for�a do pulo
    [SerializeField]
    private float jumpHeight, jumpForce = 8f;

    private Rigidbody2D playerRigidBody;

    private PlayerInput playerInput;
    private Vector2 horizontalInput;

    private GroundCheck groundCheck;

    SpriteRenderer playerFlip;
    private Animator pAnimator;

    // Inicializa��o das vari�veis
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
        // Movimenta��o do player
        playerRigidBody.velocity = new Vector2(horizontalInput.x * velocity, playerRigidBody.velocity.y);

        // Flip do sprite do player
        if (horizontalInput.x > 0)
        {
            playerFlip.flipX = false;
        }
        else if (horizontalInput.x < 0)
        {
            playerFlip.flipX = true;
        }
    }

    // M�todo chamado pelo input system (WASD)
    public void OnMovement(InputAction.CallbackContext context)
    {
        // Pega o valor do input
        horizontalInput = context.ReadValue<Vector2>();

        // se o eixo x do player for 0 roda anima��o idle, se for diferente disso roda a anima��o run
        if (horizontalInput.x == 0)
        {
            pAnimator.Play("PlayerIdle");
        }
        else
        {
            pAnimator.Play("PlayerRun");
        }
    }

    // M�todo chamado pelo input system (Space)
    public void OnJump(InputAction.CallbackContext context)
    {
        // Ap�s verificar as condi��es de pulo, solta a anima��o jump
        if (context.phase == InputActionPhase.Started && groundCheck.grounded)
        {
            //float newY = transform.position.y + jumpHeight;
            //transform.Translate(new Vector3(0, newY, 0) * Time.deltaTime);

            playerRigidBody.velocity = Vector2.up * jumpForce;

            pAnimator.Play("PlayerJump");
        }
        // verifica��o do segundo pulo para refazer anima��o jump
        else if (context.phase == InputActionPhase.Started && !groundCheck.grounded && playerRigidBody.velocity.y > 0)
        {
            playerRigidBody.velocity = Vector2.up * jumpForce;
            pAnimator.Play("PlayerJump");
        }
        // qd para de aumentar o valor y solta a anima��o fall
        else if (context.phase == InputActionPhase.Canceled && playerRigidBody.velocity.y > 0)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y * 0.5f);

            pAnimator.Play("PlayerFall");
        }
    }
}
