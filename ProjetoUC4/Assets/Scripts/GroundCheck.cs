using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Caso um objeto com tag Ground entre no collider, grounded recebe true
        if (collision != null && collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Caso um objeto com tag Ground entre e fique no collider, grounded recebe true
        if (collision != null && collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Caso um objeto com tag Ground saia do collider, grounded recebe false
        if (collision != null && collision.gameObject.tag == "Ground")
            grounded = false;
    }
}
