using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Script_1 : MonoBehaviour
{

    //outros
    public GameObject bullets;
    public Transform weapon;
    public bool canFire;
    private float timer;
    public float timeFire;

    //mira do mouse

    private Camera mainCam;
    private Vector3 mousePos;


    void Start()
    {
        // aqui estou dizendo para buscar meu main camera e acessar o componete camera dele
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        // sreen to world point vai transformar minha tela em um lugar onde posso apontar com meu mouse
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        /* mathf.red2deg = convertendo uradiano em grau
         * mathf.atan2 = calcular angulo
         * 
         * aqui estou calculando angulo da rotacao do personagm
         */
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;


        //aqui apliquei o quaternion porque e usado para funcao de rotacao e euler para voltar um valor na rotacao z 
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        Fire();

    }

    public void Fire()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeFire)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && canFire)
        {
            canFire = false;
            Instantiate(bullets, weapon.position, Quaternion.identity);
            
        }
    }

}