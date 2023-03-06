using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Script_5 : MonoBehaviour
{
    //tudo que eu preciso para atirar o prefab
    public int damage;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rigby;
    public float velociity;

    void Start()
    {
        //aqui estou dizendo para pegar a camera do unity e dar o componete camera 
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //aqui a munição vai receber o rigidbody
        rigby = GetComponent<Rigidbody2D>();

        //aqui estou fazendo o mesmo esquema do screentoword para a muniçao atirar onde eu apontar com o mouse
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        //aqui eu estou dizendo para receber a velocidade da trajetoria da muniçao obs: normalized para que ele tenha a mesam velocidade em todas as direções
        rigby.velocity = new Vector2(direction.x, direction.y).normalized * velociity;

        //aqui e a mesma coisa que fiz no script 1 e nao sei explicar direito oque eu fiz ksskskskskks
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }


    void Update()
    {
        //aqui estou dizendo para destruir o objeto apos 4 segundo para que nao fique na tela para sempre
        Destroy(gameObject, 4);
    }




    public void OnTriggerEnter2D(Collider2D collision)

    {
        /*aqui e a continuação do dano
         * aqui implementei um collider para dar dano no inimigo
         * collider vai pegar o componente "enemyhealth" para tentar achar o "takedamage" assim conseguindo dar dano no inimigo
        */

        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyComponent))
        {

            enemyComponent.TakeDamage(damage);
            Destroy(this.gameObject);

        }
    }

}
