using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class WeaponController : MonoBehaviour
{
    private Weapon weapon;
    // private int currentWeapon;

    public Canvas canvasControll;
    public TextMeshProUGUI reloadText;
    public TextMeshProUGUI text;

    int bulletsLeft, bulletShot, taps;

    bool shooting, readyToShoot, reloading;

    //mira do mouse
    private Camera mainCam;
    private Vector3 mousePos;

    private float lastShot;

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
        // pega posi��o do mouse para mira da arma
        mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Rota��o da arma; Tem q ser direto na arma, sen�o roda na posi��o errada
        weapon.Rotate(mousePos);

        ResetShoot();

        // verifica se est� atirando
        if (readyToShoot && (shooting || taps > 0) && !reloading && bulletsLeft > 0)
        {
            // o tiro vai receber tiro por aperto assim atirando apenas certa quantidade de muni��o
            bulletShot = weapon.bulletsPerTap;
            Fire();
        }
        
        //textmeshpro para mostrar a muni��o na tela
        //text.SetText(bulletsLeft + "/" + magazineSize);
        DisableText();
        text.SetText(bulletsLeft + "/" + weapon.magazineSize);

        if (bulletsLeft <= 0)
        {

            EnableText();
            reloadText.text = "Press R to Reload";

        }
    }

    private void ChangeGun()
    {
        // pega os par�metros da arma
        weapon = gameObject.GetComponentInChildren<Weapon>();

        // aqui onde vai come�ar com muni��o cheio e vai ativar o bool "readytoshoot" para true
        bulletsLeft = weapon.magazineSize;
        readyToShoot = true;
        taps = 0;
    }

    // M�todo chamado pelo input system (Mouse0)
    public void OnShoot(InputAction.CallbackContext context)
    {
        // aqui se eu deixar o bool "allowbuttomhold" true pode segurar o botao de atirar que o player vai continuar atirando
        shooting = (weapon.allowButtonHold && context.interaction is HoldInteraction && context.phase == InputActionPhase.Performed) ||
            (context.interaction is TapInteraction && context.phase == InputActionPhase.Started);

        if (!weapon.allowButtonHold && shooting && bulletsLeft > 0 && taps <= 0)
        {
            taps += weapon.bulletsPerTap;
        }
    }

    public void Fire()
    {
        if (bulletShot > 0 && bulletsLeft > 0 && Time.time - lastShot >= weapon.timeBetShoot)
        { 
            // Desabilita o tiro
            readyToShoot = false;

            // Instancia a bala
            Instantiate(weapon.bullets, weapon.transform.position, Quaternion.identity);

            // caso o player atire, ira diminuir a muni��o 
            bulletsLeft--;
            bulletShot--;

            taps--;

            // Salva tempo do �ltimo tiro
            lastShot = Time.time;
        }
    }

    public void ResetShoot()
    {
        // Se j� passou tempo suficiente desde o �ltimo tiro, reabilita o tiro
        if (Time.time - lastShot >= weapon.timeBetShooting)
            readyToShoot = true;
    }

    // M�todo chamado pelo input system (R)
    public void OnReload(InputAction.CallbackContext context)
    {

        if (bulletsLeft <= 0)
        {

            EnableText();

        }

        // Se h� balas suficientes e n�o estiver recarregando
        if (bulletsLeft < weapon.magazineSize && !reloading)
        {
            // se a muni��o for menor que o tamanho do carregador ele vaia tivar o bool "reloading" e vai carregar
            reloading = true;

            // quando ativar o bool ele vai entrar no evento invoke e vai chamar "reloadfinish" que vai colocar o bool no false e carregar a muni��o
            Invoke("ReloadFinish", weapon.reloadTime);
        }
    }

    public void ReloadFinish()
    {
        // o que eu expliquei la em cima resume aqui
        bulletsLeft = weapon.magazineSize;
        reloading = false;
    }

    public void EnableText()
    {
        canvasControll.gameObject.SetActive(true);
    }
    public void DisableText()
    {
        canvasControll.gameObject.SetActive(false);
    }
}
