using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    Transform jugador;//obtener referencia del transform del jugador, no es publica ya que al ser un prefab el enemigo puede perder referenciasdel inspector
    VidaJugador vidaJugador;
    VidaEnemigo vidaEnemigo;
    UnityEngine.AI.NavMeshAgent nav;//referencia al AI nav mesh agent


    void Awake()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;//buscamos la referencia al objeto jugador con este tag
        vidaJugador = jugador.GetComponent <VidaJugador> ();//Obtiene la referencia a un componente de ese transform
        vidaEnemigo = GetComponent <VidaEnemigo> ();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();//referencia la AI
    }


    void Update()
    {
        if (vidaEnemigo.nivelSalud > 0 && vidaJugador.nivelSalud > 0)
        {
            nav.SetDestination(jugador.position);//se le indica donde se encuentra el jugador por cada fotograma para que lo persiga
        }
        else
        {
            nav.enabled = false;//deshabilito el componente que hace mover al enemigo con AI
        }
    }
}
