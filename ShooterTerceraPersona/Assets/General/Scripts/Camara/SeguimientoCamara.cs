using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    public Transform objetivo;//referencia al objeto que quiero que la camara siga
    public float suavidadMovimiento = 5f;//suavidad de movimiento de seguimeinto

    Vector3 separacion;//separacion entre la camara y el persoinaje

    private void Start()
    {
        separacion = transform.position - objetivo.position;//posicion relativa del objeto a la camara
    }

    private void FixedUpdate()
    {
        Vector3 objetivoCamaraPosicion = objetivo.position + separacion;//separacion inicial reinsidente
        transform.position = Vector3.Lerp(transform.position, objetivoCamaraPosicion, suavidadMovimiento * Time.deltaTime);//Metodo que toma 3 valores 1(valor minimo) 2(valormaximo) 3(valor entre 0 y 1) retornara una valor intervaloe ntre el primer y segundo valor multiplicado por el tercer valor
    }
}
