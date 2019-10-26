using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//uso de la interfaz grafica

public class AdministrarRanking : MonoBehaviour
{
    public Canvas canvas;//variable que contendra la referencia al componente (Canvas)

    Transform entradaContenedor;//variable que contendra la referencia al componente (Transform)
    Transform entradaPlantilla;//variable que contendra la referencia al componente (Transform)
    List<Transform> entradaMarcadorListaTransform;//Lista de transforms de la clase entrada marcador
    List<EntradaMarcador> entradaMarcadorLista;
 

    private void Awake()
    {
        entradaContenedor = transform.Find("HighScoreEntryContainer");//agrego la referencia a un objeto con ese nombre
        entradaPlantilla = entradaContenedor.Find("HighScoreEntryTemplate");//agrego la referencia a un objeto con ese nombre y que sea hijo de ese objeto

        entradaPlantilla.gameObject.SetActive(false);//desactivo el objeto plantilla

        
        if (PlayerPrefs.GetString("leo") == string.Empty)//Si el playerprefers con la clave leo esta vacio
        {

            entradaMarcadorLista = new List<EntradaMarcador>()//genero una lista
            {
                new EntradaMarcador {score = 000, name ="AAA"}//la relleno con un obejto incial
            };

            string jsonEntrada = JsonUtility.ToJson(entradaMarcadorLista);//la lista la convierto en .json
            PlayerPrefs.SetString("leo", jsonEntrada);//guardo la lista json con el id leo
            PlayerPrefs.Save();//guardo la lista

        }

        string jsonString = PlayerPrefs.GetString("leo");//tomo las lista guardadas con la llave leo
        Marcadores marcadores = JsonUtility.FromJson<Marcadores>(jsonString);//convierto el .json en un objeto tipo Marcadores





        //organizando la lista por puntaje

        for (int i = 0; i < marcadores.entradaMarcadorLista.Count; i++)
        {
            for (int j = i; j < marcadores.entradaMarcadorLista.Count; j++)
            {
                if (marcadores.entradaMarcadorLista[j].score > marcadores.entradaMarcadorLista[i].score)
                {
                    EntradaMarcador tmp = marcadores.entradaMarcadorLista[i];
                    marcadores.entradaMarcadorLista[i] = marcadores.entradaMarcadorLista[j];
                    marcadores.entradaMarcadorLista[j] = tmp;
                }
            }
            
        }


        entradaMarcadorListaTransform = new List<Transform>();//instancio la lista de transforms tipo entradaMarcador

        foreach (EntradaMarcador entradaMarcador in marcadores.entradaMarcadorLista)//recorro la lista de arriba
        {
            CrearUnMarcadorTransform(entradaMarcador, entradaContenedor, entradaMarcadorListaTransform);//ejecuto el metodo por cada objeto en la lista
        }
        
    }

    public void Pause()
    {
        canvas.enabled = !canvas.enabled;//va ha cambiar el estado del canvas independientemente en el que se encuentre
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;//Si esta pausado el juedo se despausa o a la inversa
    }

    void CrearUnMarcadorTransform(EntradaMarcador entradaMarcador, Transform contenedor, List<Transform> listaTransform)// metodo en el cual se ingresaran la puntuacion y nombre, el contenedor padre, y cada contenedor o plantilla  hijo
    {
        if (listaTransform.Count>= 10)//Si hay mas de 10 valores se sale
            return;
        
        
        float AlturaPlantilla = 30f;//espacio entre cada entrada planilla nueva
        Transform entradaTransform = Instantiate(entradaPlantilla, contenedor);//instancio un nuevo transform del tipo entrada platilla en el contenedor(de tipo entrada planilla, dentro de entrada contenedor)
        RectTransform entradaRectTransform = entradaTransform.GetComponent<RectTransform>();//el transform lo convierto en rectransform
        entradaRectTransform.anchoredPosition = new Vector2(0, -AlturaPlantilla * listaTransform.Count);//poscion del pivot del nuevo rectransfom (horizontal,vertical por cada nuevo se le multiplica el indice de la lista)
        entradaTransform.gameObject.SetActive(true);//activo el nuevo rectransfom

        int rank = listaTransform.Count + 1;//variable que se crea igualando por el indice de la lista + 1
        string rankString;//cadena de texto

        switch (rank)//se evaluara el indice
        {
            case 1: rankString = "1ST"; break;//en cas de que el indice sea 1 se le colocara ese texto ala variable
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;//en caso de que no sea ninguno de los 3 se le añadira un th a las siguientes posiciones

        }

        entradaTransform.Find("Posicion").GetComponent<Text>().text = rankString;//Busco el objeto con ese nombre el cual es un tipo text y le asigno el nombre de la cadena de texto rankString

        int score = entradaMarcador.score;//toma el marcador ingresado de la instancia
        entradaTransform.Find("Score").GetComponent<Text>().text = score.ToString();//Busco el objeto con ese nombre el cual es un tipo text y le asigno el nombre de la cadena de texto rankString

        string name = entradaMarcador.name;//toma el nombre incresado de la instancia
        entradaTransform.Find("Name").GetComponent<Text>().text = name;//Busco el objeto con ese nombre el cual es un tipo text y le asigno el nombre de la cadena de texto rankString

        listaTransform.Add(entradaTransform);//añado a la lista la plantilla generada
        
    }

    //Metodo para añadir daos al marcador
    public void AñadirMarcador(int score, string name)
    {

        EntradaMarcador entradaMarcador = new EntradaMarcador { score = score, name = name };//igualos los datos de entrada a los de la clase

        string jsonString = PlayerPrefs.GetString("leo");//tomo las lista guardadas con la llave leo
        Marcadores marcadores = JsonUtility.FromJson<Marcadores>(jsonString);//convierto el .json en un objeto tipo Marcadores

        marcadores.entradaMarcadorLista.Add(entradaMarcador);//añado marcador

        string json = JsonUtility.ToJson(marcadores);//convierte un objeto o lista de objetos en un formato .json para esto la clase del objeto debe ser serializable [System.Serializable]
        PlayerPrefs.SetString("leo", json);//guardo la lista json con el id leo
        PlayerPrefs.Save();//guardo la lista
    }

    [System.Serializable]
    class EntradaMarcador
    {
        public int score;
        public string name;
    }

    //clase que contiene marcadores
    class Marcadores
    {
        public List<EntradaMarcador> entradaMarcadorLista;
    }
    

}
