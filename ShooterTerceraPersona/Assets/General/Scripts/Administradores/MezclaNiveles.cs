using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;//Espacio de nombre que sirve para poder interactuar con audioMixer 

public class MezclaNiveles : MonoBehaviour
{

    public AudioMixer masterMixer;// variable que contendra la referencia al componente (audioMixer)



    //Metodo que actualizara el valor del parametro expuesto del grupo EfectosSonido, con el parametro que se le pase en el on value change
    public void setVolumenEfectosSonido(float efectosVolumen)
    {
        masterMixer.SetFloat("VolumenEfectoSonido",efectosVolumen);
    }


    //Metodo que actualizara el valor del parametro expuesto del grupo Musica, con el parametro que se le pase en el on value change
    public void setVolumenMusica(float musicaVolumen)
    {
        masterMixer.SetFloat("VolumenMusica",musicaVolumen);
    }

}
