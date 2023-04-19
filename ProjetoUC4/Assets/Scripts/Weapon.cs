using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public GameObject bullets;
    public Transform weapon;

    public int magazineSize, bulletsPerTap;

    public bool allowButtonHold;

    public float timeBetShooting, reloadTime, timeBetShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(Vector3 mousePos)
    {
        Vector3 rotation = mousePos - transform.position;

        /* mathf.red2deg = convertendo uradiano em grau
         * mathf.atan2 = calcular angulo
         * 
         * aqui estou calculando angulo da rotacao do personagm
         */
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;


        //aqui apliquei o quaternion porque e usado para funcao de rotacao e euler para voltar um valor na rotacao z 
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
