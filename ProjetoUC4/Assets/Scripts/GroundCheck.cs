using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Verifica se está pulando ou não
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Ground")
            grounded = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Ground")
            grounded = false;
    }
}
