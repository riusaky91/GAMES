using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR //Este codigo entre # solo se ejecutara cuando nos encontremos en el editor de Unity
using UnityEditor;
#endif
using UnityEngine.Audio;//Espacio de nombre que sirve para poder interactuar con audioMixer 

public class AdministrarPause : MonoBehaviour {

    public Slider SliderVolumenMusica;//variable que contendra la referencia al componente (slider)
    public Slider SliderVolumenEfectos;//variable que contendra la referencia al componente (slider)

    public AudioMixerSnapshot pausa;// variable que contendra la referencia al componente (snapShot del audio Mixer)
    public AudioMixerSnapshot desPausa;// variable que contendra la referencia al componente (snapShot del audio Mixer)

    Canvas canvas;//variable que contendra la referencia al componente (Canvas)

    void EstadoCargado()
    {
        SliderVolumenMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 0f);//cargo los estados en que se encuentran los sliders de volumen en el canvas
        SliderVolumenEfectos.value = PlayerPrefs.GetFloat("VolumenEfectos", 0f);
    }

    void EstadoGuardado()
    {
        PlayerPrefs.SetFloat("VolumenMusica", SliderVolumenMusica.value);//guardo los estados en que se encuentran los sliders de volumen
        PlayerPrefs.SetFloat("VolumenEfectos", SliderVolumenEfectos.value);
    }

    void Start()
    {
        canvas = GetComponent<Canvas>();//haciendo referencia al componente (Canvas)
        canvas.enabled = false;//Deshabilito el componente canvas
        EstadoCargado();//cargo el panel con la informacion guardada de los estados de cada slider
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//Si oprimimos la tecla Esc en el teclado
        {
            Pause();//Ejecuto metodod pause
        }
    }
    

    //Metodo para pausar el juego
    public void Pause()
    {
        canvas.enabled = !canvas.enabled;//va ha cambiar el estado del canvas independientemente en el que se encuentre
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;//Si esta pausado el juedo se despausa o a la inversa

        if (!canvas.enabled)//Si el canvas no esta visible
        {

            EstadoGuardado();//guardo los estados en que se encuentran los sliders de volumen
            desPausa.TransitionTo(0.01f);//Cambia el snapshot del audiomixer a despausa en el tiempo dado
        }
        else
        {
            pausa.TransitionTo(0.01f);//Cambia el snapshot del audiomixer a pausa en el tiempo dado
        }
    }
    

    //metodo apra salir del Juego
    public void Salir()
    {
        #if UNITY_EDITOR //Este codigo entre # solo se ejecutara cuando nos encontremos en el editor de Unity
        EditorApplication.isPlaying = false;//Detiene la ejecucion de la aplicacion
        #else 
        Application.Quit();//salgo de la aplicacion
        #endif
        EstadoGuardado();//guardo los estados en que se encuentran los sliders de volumen

    }
}