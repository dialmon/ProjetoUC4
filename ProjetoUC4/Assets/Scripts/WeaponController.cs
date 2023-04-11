using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class WeaponController : MonoBehaviour
{
    private Weapon weapon;
    // private int currentWeapon;

    int bulletsLeft, bulletShot;

    bool shooting, readyToShoot, reloading;

    //mira do mouse
    private Camera mainCam;
    private Vector3 mousePos;

    // textmesh muni��o
    // public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Awake()
    {
        // Inicializando arma
        ChangeGun();

        // aqui estou dizendo para buscar meu main camera e acessar o componete camera dele
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // sreen to world point vai transformar minha tela em um lugar onde posso apontar com meu mouse
        mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Rota��o da arma; Tem q ser direto na arma, se n�o roda na posi��o errada
        weapon.Rotate(mousePos);

        //textmeshpro para mostrar a muni��o na tela
        //text.SetText(bulletsLeft + "/" + magazineSize);
    }

    private void ChangeGun()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();

        //aqui onde vai come�ar com muni��o cheio e vai ativar o bool "readytoshoot" para true
        bulletsLeft = weapon.magazineSize;
        readyToShoot = true;
    }


    public void OnShoot(InputAction.CallbackContext context)
    {
        // aqui se eu deixar o bool "allowbuttomhold" true pode segurar o botao de atirar que o player vai continuar atirando
        if (weapon.allowButtonHold)
        {

            shooting = context.interaction is HoldInteraction;
        }
        else
        {
            //caso contrario se bool for false precisa apertar varias vezes para atirar
            shooting = context.interaction is TapInteraction;
        }

        // aqui eu fiz essa gambiarra para fazer uma mecanica de burst fire que e para atirar 3 muni��o apenas
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //o tiro vai receber tiro por aperto assim atirando apenas certa quantidade de muni��o
            bulletShot = weapon.bulletsPerTap;
            Fire();
        }
    }
    public void Fire()
    {
        //aqui onde faz o player atirar o prefab bullet 
        readyToShoot = false;

        Instantiate(weapon.bullets, weapon.transform.position, Quaternion.identity);

        //caso o player atire ira diminuir a muni��o 
        bulletsLeft--;
        bulletShot--;

        //aqui uso o evento invoke para resetar meu tiro e poder atirar novamente com o tempo entre tiros assim nao atirando muito rapido
        Invoke("ResetShoot", weapon.timeBetShooting);


        if (bulletShot > 0 && bulletsLeft > 0)
        {
            Invoke("Fire", weapon.timeBetShoot);
        }
    }

    public void ResetShoot()
    {
        //como dito la em cima essa parte e para resetar meu tiro 
        readyToShoot = true;
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        //aqui fiz uma mecanica de recarregar a arma 
        if (bulletsLeft < weapon.magazineSize && !reloading)
        {
            //se a muni��o for menor que o tamanho do carregador ele vaia tivar o bool "reloading" e vai carregar
            reloading = true;

            //quando ativar o bool ele vai entrar no evento invoke e vai chamar "reloadfinish" que vai colocar o bool no false e carregar a muni��o
            Invoke("ReloadFinish", weapon.reloadTime);
        }
    }

    public void ReloadFinish()
    {
        //oque eu expliquei la em cima resume aqui
        bulletsLeft = weapon.magazineSize;
        reloading = false;
    }
}
