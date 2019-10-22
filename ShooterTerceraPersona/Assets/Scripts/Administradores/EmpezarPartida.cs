using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Biblioteca para la gestion de escenas
using UnityEngine.UI;//para usar elemntos de la interfaz

public class EmpezarPartida : MonoBehaviour
{
    public ElementoInteractivo pantalla;//referencia al script

    public Text nombreUsuario;

    public static string nombre;//variable estatica que va ser accedida desde cualquier componente llamando a su clase

    private void Awake()
    {
        Debug.Log(nombre);
    }




    // Update is called once per frame
    void Update()
    {
        if (/*Input.GetButtonDown("Fire1")*/pantalla.pulsado)//si se pulsa el eje fuego 1 o se da click 
        {
            //Vidas.vidas = 3;//reinicio el valor de vidas
            //EmpezarPartida.nombre = string.Empty;//reinicio el nombre
            AdministrarPuntuacion.puntuacion= 0;//reinicio el valor de puntos
            SceneManager.LoadScene("Nivel01");// cambia laa escena nivel01
        }
 
    }


    public void TomarUsuario()
    {
        nombre = nombreUsuario.text;
        //Debug.Log(nombre);
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");// cambia la escena creditos
    }

}
