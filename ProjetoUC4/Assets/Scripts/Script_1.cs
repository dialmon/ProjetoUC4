using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Script_1 : MonoBehaviour
{

    //tudo que e necessario para atirar
    public GameObject bullets;
    public Transform weapon;
    public TextMeshProUGUI reloadInfoText;

    public int magazineSize, bulletsPerTap;
    int bulletsLeft, bulletShot;

    public bool allowButtonHold;
    bool shooting, readyToShoot, reloading;

    private float timer;
    public float    timeBetShooting, reloadTime, timeBetShoot;


    //mira do mouse

    private Camera mainCam;
    private Vector3 mousePos;

    //textmesh

    public TextMeshProUGUI text;


    void Start()
    {
        // aqui estou dizendo para buscar meu main camera e acessar o componete camera dele
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    void Awake()
    {
        //aqui onde vai começar com munição cheio e vai ativar o bool "readytoshoot" para true
        bulletsLeft = magazineSize;
        readyToShoot = true;
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

        myInput();
        Reload();

        //textmeshpro para mostrar a munição na tela
        text.SetText(bulletsLeft + "/" + magazineSize);

    }
    public void myInput()
    {

        // aqui se eu deixar o bool "allowbuttomhold" true pode segurar o botao de atirar que o player vai continuar atirando
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            //caso contrario se bool for false precisa apertar varias vezes para atirar
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        // aqui eu fiz essa gambiarra para fazer uma mecanica de burst fire que e para atirar 3 munição apenas
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //o tiro vai receber tiro por aperto assim atirando apenas certa quantidade de munição
            bulletShot = bulletsPerTap;
            Fire();
        }
    }
    public void Fire()
    {
        //aqui onde faz o player atirar o prefab bullet 
        readyToShoot = false;

        Instantiate(bullets, weapon.transform.position, Quaternion.identity);

        //caso o player atire ira diminuir a munição 
        bulletsLeft--;
        bulletShot--;

        //aqui uso o evento invoke para resetar meu tiro e poder atirar novamente com o tempo entre tiros assim nao atirando muito rapido
        Invoke("ResetShoot", timeBetShooting);


        if (bulletShot > 0 && bulletsLeft > 0)
        {
            Invoke("Fire", timeBetShoot);
            
        }

        
    }

    public void ResetShoot()
    {
        //como dito la em cima essa parte e para resetar meu tiro 
        readyToShoot = true;
    }

    public void Reload()
    {
        reloadInfoText.text = string.Format("Press R to Reload ");
        //aqui fiz uma mecanica de recarregar a arma 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            //se a munição for menor que o tamanho do carregador ele vaia tivar o bool "reloading" e vai carregar
            reloading = true;
            

            //quando ativar o bool ele vai entrar no evento invoke e vai chamar "reloadfinish" que vai colocar o bool no false e carregar a munição
            Invoke("ReloadFinish", reloadTime);
        }
    }

    public void ReloadFinish()
    {
        //oque eu expliquei la em cima resume aqui
        bulletsLeft = magazineSize;
        reloading = false;
    }

}