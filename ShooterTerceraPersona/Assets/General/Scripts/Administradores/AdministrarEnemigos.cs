using UnityEngine;

public class AdministrarEnemigos : MonoBehaviour
{
    public VidaJugador vidaJugador;//variable para referenciar el componente (Script)
    public GameObject enemigo;//variable para referenciar al objeto enemigo (Prefab)
    public float tiempoDeAparicion = 3f;//tiempo en el que se va a generar un nuevo enemigo
    public Transform[] puntosDeAparicion;//Arreglo que contendra los puntos del escenario en los cuales aparecera un enemigo


    void Start ()
    {
        InvokeRepeating ("Aparicion", tiempoDeAparicion, tiempoDeAparicion);//Metodo que invoca el metodo Aparicion en un tiempo determinado
    }

    

    void Aparicion ()
    {
        if(vidaJugador.nivelSalud <= 0f)//Si el jugador tuene un nivel de vida menor a 0
        {
            return;//salir
        }

        int indicePuntoDeAparicion = Random.Range (0, puntosDeAparicion.Length);//Genero un numero al azar entre 0 y la cantidad de puntos de aparicion que tenga

        Instantiate (enemigo, puntosDeAparicion[indicePuntoDeAparicion].position, puntosDeAparicion[indicePuntoDeAparicion].rotation);//Instancio el objeto en la posicion predeterminada 
    }
}
