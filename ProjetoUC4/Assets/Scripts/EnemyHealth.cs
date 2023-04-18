using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    // esse script criei para que os inimigo receba dano da muni��o

    public int life, maxLife = 100;

    private float hitTime = 0f; // tempo que a anima��o de "TakeHit" est� ativa
    private float hitDuration = 1f; // dura��o da anima��o de "TakeHit"

    private Animator anim; // refer�ncia ao Animator do inimigo

    // Start is called before the first frame update
    void Start()
    {
        //aqui eu falo para que o inimigo comece recebendo a vida maxima dito para a vida base do inimigo
        life = maxLife;

        // Obter o componente Animator do inimigo
        anim = GetComponent<Animator>();

        gameObject.GetComponent<Animator>().SetBool("TakeHit", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Se o tempo de anima��o de "TakeHit" for menor que a dura��o, ativar a anima��o
        if (hitTime < hitDuration)
        {
            anim.SetBool("TakeHit", true);
            hitTime += Time.deltaTime; // Adicionar tempo desde a �ltima atualiza��o
        }
        else
        {
            anim.SetBool("TakeHit", false);
        }
    }

    //int damage e para tomar dano do "damage" do script da muni��o 
    public void TakeDamage(int damage)
    {
        //aqui onde tudo come�a para tomar dano
        life -= damage;

        hitTime = 0f; // Resetar o tempo de anima��o de "TakeHit"

        //se a vida for igual a 0 vai destruir o objeto
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

}