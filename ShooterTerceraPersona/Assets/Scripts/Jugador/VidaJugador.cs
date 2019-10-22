using UnityEngine;
using UnityEngine.UI;//es necesario ya que se referencia objetos que pertenecen al Canvas
using System.Collections;
using UnityEngine.SceneManagement;


public class VidaJugador : MonoBehaviour
{
    public int saludInicial = 100;//vida inicial del jugador
    public int nivelSalud;//nivel de salud del jugador en cada momento
    public Slider saludSlider;//referencia al slider
    public Image dañoImagen;// daño del jugador en pantalla completa
    public AudioClip clipMuriendo;//Clip de sonido del jugador muriendo
    public float velocidadColor = 5f;//Velocidad en la que el color se ira haciendo transparente
    public Color colorPantalla = new Color(1f, 0f, 0f, 0.1f);//para pintar la pantalla
    public AdministrarRanking administrarRanking;


    Animator anim;//reeferencia al animator
    AudioSource reproducirAudio;//referencia al audio source del personaje
    MovimientoJugador movimientoJugador;//referencia  la clase movimiento del jugador
    JugadorDisparando jugadorDisparando;
    bool estaMuerto;
    bool daño;//cada vez que un enemigo le haga daño al personaje


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        reproducirAudio = GetComponent <AudioSource> ();
        movimientoJugador = GetComponent <MovimientoJugador> ();
        jugadorDisparando = GetComponentInChildren <JugadorDisparando> ();
        nivelSalud = saludInicial;
    }


    void Update ()
    {
        if(daño)//si el daño es verdadero
        {
            dañoImagen.color = colorPantalla;//la imagen toma el color que generamos
        }
        else
        {
            dañoImagen.color = Color.Lerp (dañoImagen.color, Color.clear, velocidadColor * Time.deltaTime);//El color de la imagen se va interpolar con Lerp en un tiempo especifico
        }
        daño = false;
    }


    public void RecibirDaño (int valor)//recibe un parametro
    {
        daño = true;//daño a veradero para que se pinte la pantalla 

        nivelSalud -= valor;//baja el nivel de salud dependiendo el valor que se le haya dado

        saludSlider.value = nivelSalud;//el valor de la salud se v amostrar en el slider

        reproducirAudio.Play ();//reproduce el sonido del audio

        if(nivelSalud <= 0 && !estaMuerto)//se pregunta si esta muerto
        {
            Muerte ();//metodo muera
        }
    }


    void Muerte ()
    {
        estaMuerto = true;//pasa a verdadero 

        if (EmpezarPartida.nombre == null)//si no se rrelleno un nombre 
        {
            administrarRanking.AñadirMarcador(AdministrarPuntuacion.puntuacion, "Leo");//añado la puntuacion al marcador y leo como nombre
        }
        else
        {
            administrarRanking.AñadirMarcador(AdministrarPuntuacion.puntuacion, EmpezarPartida.nombre);//añado la puntuacion al marcador y el nombre asignado
        }


        jugadorDisparando.DeshabilitarEfectos ();//el efecto Line Renderer y el efecto luz se deshabilitan

        anim.SetTrigger ("Muerte");//ejecuto la animacion teniendo en cuenta el trigger de la transicion

        reproducirAudio.clip = clipMuriendo;//añado el clip muriendo al audio source
        reproducirAudio.Play ();//reproduzco el audio 

        movimientoJugador.enabled = false;// para que le jugador no se mueva despeues de morir
        jugadorDisparando.enabled = false;// El componnente script se deshabilita
    }

    //Metodo que genera una corrutina y es llamado por la animacion de muerte del Jugador

    public void ProgramarReiniciarNivel()
    {
        StartCoroutine(ReiniciarNivel());
    }

    //Corrutina que se ejecuta despues de pasa 4 segundos y reinicia el nivel
    IEnumerator ReiniciarNivel()
    {

        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }

}
