using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    // esse script criei para que os inimigo receba dano da munição

    public int life, maxLife = 100;

    // Start is called before the first frame update
    void Start()
    {
        //aqui eu falo para que o inimigo comece recebendo a vida maxima dito para a vida base do inimigo
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
              
    }

    //int damage e para tomar dano da "damage" do script da munição 
    public void TakeDamage(int damage)
    {
        //aqui onde tudo começa para tomar dano
        life -= damage;

        //se a vida for igual a 0 vai destruir o objeto
        if (life <= 0) 
        {
            Destroy(gameObject);
        }
    }

}
