using UnityEngine;
using System.Collections;

public class AtaqueEnemigo : MonoBehaviour
{
    public float tiempoEntreAtaques = 0.5f;//Tiempo que va pasar entre ataques del enmigo
    public int dañoAtaque = 10;//dañoq eu va hacer el enemigo


    Animator anim;//referencia al animator del enmeigo
    GameObject jugador;//refrencia al gameobject jugador
    VidaJugador vidaJugador;//referencia al script palyerHealt
    VidaEnemigo vidaEnemigo;
    bool jugadorEnRango;//valida si el jugador se encuentra en el rango de ataque
    float tiempo;//reloj de tiempo que pasara entre cada ataque



    void Awake ()
    {
        jugador = GameObject.FindGameObjectWithTag ("Player");//genera la referencia al objeto con este tag
        vidaJugador = jugador.GetComponent <VidaJugador> ();//genera la referencia al script
        vidaEnemigo = GetComponent<VidaEnemigo>();
        anim = GetComponent <Animator> ();//genera referencia a la animacion
        
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == jugador)//si el objto que entro en el rango es el jugador
        {
            jugadorEnRango = true;//se modifica al validador
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == jugador)//si el objeto que salio es el jugador
        {
            jugadorEnRango = false;//se modifica el validador
        }
    }


    void Update ()
    {
        tiempo += Time.deltaTime;//el timepo irá corriendo

        if(tiempo >= tiempoEntreAtaques && jugadorEnRango && vidaEnemigo.nivelSalud> 0)//si el timepo que corre es mayor al tiempo entre ataque y el jugador en rango es verdadero y el enemigo tiene una vida mayo a cero
        {
            Atacar ();//Metodo para atacar
        }

        if(vidaJugador.nivelSalud <= 0)//si la vida del jugador es menor o igula a cero
        {
            anim.SetTrigger ("JugadorMuerto");//anima la muerte del jugador con el trigger de la transiicion
            
        }
    }


    void Atacar()
    {
        tiempo = 0f;//reinicia el tiempo a cero

        if(vidaJugador.nivelSalud > 0)//si la vida del jugador es mayor a cero
        {
            vidaJugador.RecibirDaño (dañoAtaque);//va ha recibir daño con el valor dado 
        }
    }
}
