using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdministrarGameOver : MonoBehaviour
{
    public VidaJugador vidaJugador;//variable que contendra la referencia al componente (Script)


    Animator anim;//variable que contendra la referencia al componente (Animator)

    //Metodo para refenciar los componentes

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (vidaJugador.nivelSalud <= 0)//Si la salud es menor a 0
        {
            anim.SetTrigger("GameOver");//Ejecuta el trigger que reproduce la animacion del gameOver            
        }
    }
}
