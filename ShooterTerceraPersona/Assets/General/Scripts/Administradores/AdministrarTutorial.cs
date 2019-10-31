using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministrarTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LimpiarTutorial());
        
    }    
    

    //Corrutina que se ejecuta despues de pasa 4 segundos y reinicia el nivel
    IEnumerator LimpiarTutorial()
    {

        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
        //SceneManager.LoadScene(0);
    }
}
