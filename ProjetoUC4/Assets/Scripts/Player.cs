using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    // Velocidade da movimentação
    [SerializeField]
    private float velocity;

    // Altura e força do pulo
    [SerializeField]
    private float jumpHeight, jumpForce = 8f;

    private Rigidbody2D playerRigidBody;

    private PlayerInput playerInput;
    private Vector2 horizontalInput;

    private GroundCheck groundCheck;

    SpriteRenderer playerFlip;
    private Animator pAnimator;

    // Inicialização das variáveis
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
        // Movimentação do player
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

    // Método chamado pelo input system (WASD)
    public void OnMovement(InputAction.CallbackContext context)
    {
        // Pega o valor do input
        horizontalInput = context.ReadValue<Vector2>();

        // se o eixo x do player for 0 roda animação idle, se for diferente disso roda a animação run
        if (horizontalInput.x == 0)
        {
            pAnimator.Play("PlayerIdle");
        }
        else
        {
            pAnimator.Play("PlayerRun");
        }
    }

    // Método chamado pelo input system (Space)
    public void OnJump(InputAction.CallbackContext context)
    {
        // Após verificar as condições de pulo, solta a animação jump
        if (context.phase == InputActionPhase.Started && groundCheck.grounded)
        {
            //float newY = transform.position.y + jumpHeight;
            //transform.Translate(new Vector3(0, newY, 0) * Time.deltaTime);

            playerRigidBody.velocity = Vector2.up * jumpForce;

            pAnimator.Play("PlayerJump");
        }
        // verificação do segundo pulo para refazer animação jump
        else if (context.phase == InputActionPhase.Started && !groundCheck.grounded && playerRigidBody.velocity.y > 0)
        {
            playerRigidBody.velocity = Vector2.up * jumpForce;
            pAnimator.Play("PlayerJump");
        }
        // qd para de aumentar o valor y solta a animação fall
        else if (context.phase == InputActionPhase.Canceled && playerRigidBody.velocity.y > 0)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y * 0.5f);

            pAnimator.Play("PlayerFall");
        }
    }
}
