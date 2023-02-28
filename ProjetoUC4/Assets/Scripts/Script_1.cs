using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;

public class Script_1 : MonoBehaviour
{

    //outros
    public GameObject bullets;
    public Transform weapon;

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

        text.SetText(bulletsLeft + "/" + magazineSize);

    }
    public void myInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletShot = bulletsPerTap;
            Fire();
        }
    }
    public void Fire()
    {
        readyToShoot = false;

        Instantiate(bullets, weapon.transform.position, Quaternion.identity);

        bulletsLeft--;
        bulletShot--;

        Invoke("ResetShoot", timeBetShooting);

        if (bulletShot > 0 && bulletsLeft > 0)
        {
            Invoke("Fire", timeBetShoot);
        }
    }

    public void ResetShoot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            reloading = true;
            Invoke("ReloadFinish", reloadTime);
        }
    }

    public void ReloadFinish()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}