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
        // TODO - Aumentar EdgeRadius no BoxCollider do Player
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
    }

    void Jump()
    {
        // Tutorial pulo: https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/
        // Documentação Input system: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/index.html
        // TODO - restringir double jump
        if (Input.GetButton("Jump"))
        {
            playerRigidBody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

    }
}
