using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdministrarPuntuacion : MonoBehaviour
{
    public static int puntuacion;//variable estatica que va ser accedida desde cualquier componente llamando a su clase


    Text texto;//variable para referenciar el componente (Text)



    //inicializacion de varibles
    void Awake ()
    {
        texto = GetComponent <Text> ();
        puntuacion = 0;//la inicializo en 0
    }


    void Update ()
    {
        texto.text = "Puntuacion: " + puntuacion;//el parametro text del componente Text se modificara en cada fotograma con el valor de la puntuacion
    }
}
