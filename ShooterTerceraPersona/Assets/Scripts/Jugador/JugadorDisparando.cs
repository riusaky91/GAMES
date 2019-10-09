using UnityEngine;
using UnityEngine.UI;//Se utiliza cuando se referencien componentes del canvas
using System.Collections;//Se utiliza para generar corrutinas
using UnityEngine.EventSystems;//Se utiliza para trabajar on eventos del escenario como botones


public class JugadorDisparando : MonoBehaviour
{
    public int dañoPorDisparo = 20;//Variable que guarda el dañoq ue va hacer cada disparo 
    public float tiempoEntreBalas = 0.15f;//varable que indica el tiempo que va haber entre balas
    public float rango = 100f;//variable que contiene el rango de disparo
    public Slider municionSlider;// variable que contendra la referencia al componente(Slider) de la Municion
    public int balasMaximas;//Variable que contendra la cantidad maxima de balas del jugador
    public Image imagenEnemigo;// variable que contendra la referencia al componente(Image) la cual se modificara por cada enmigo que se le dispare
    public Slider vidaSliderEnemigo;//// variable que contendra la referencia al componente(Slider) del enemigo a disparar


    float timer;//contador de tiempo que ha pasado desde el ultimo disparo
    Ray disparoRayo = new Ray();//descripcion de punto inicial y direcion en la que se va simular el disparo de rayo ( ray es un tipo estructure por lo tanto no necesita un new y demas es como un float)
    RaycastHit objetivoRayo;//resultado del objeto que colisiona con la bala
    int mascaraDisparable;//Capa con la que queremos que la bala colisione
    ParticleSystem particulasArma;//variable que contendrá particulas que tendra el arma 
    LineRenderer lineaDisparo;//variable que cntendra el componente line rendere
    AudioSource audioArma;//variable que contendra el audioo
    Light luzArma;//variable que contendra la luz
    float efectosVisualizacion = 0.2f;//tiempo en el que van a estar activadas la luz y la linea de disparo
    int nivelBalas;//Nivel de la municion en cada momento

    //Hago referencia a los componentes de cada tipo
    void Awake ()
    {
        mascaraDisparable = LayerMask.GetMask ("Shootable");
        particulasArma = GetComponent<ParticleSystem> ();
        lineaDisparo = GetComponent <LineRenderer> ();
        audioArma = GetComponent<AudioSource> ();
        luzArma = GetComponent<Light> ();
        nivelBalas = balasMaximas;//Igualo el valor del nivel de balas a su amximo al iniciar la escena
        StartCoroutine(RecargarMunicion());//Inicializo la corrutina para la recarga de municion
        imagenEnemigo.enabled = false;//Deshabilito la imagen por defecto
        vidaSliderEnemigo.gameObject.SetActive(false);//desactivo el objeto Slider inicialmente
    }


    void Update ()
    {
        timer += Time.deltaTime;//el timepo se ira actualizando ty aumentando constantemente

		if(Input.GetButton ("Fire1") && timer >= tiempoEntreBalas && Time.timeScale != 0 && !EventSystem.current.IsPointerOverGameObject(-1))//si oprimimos el boton Fire y el tiempo es mayo o igualal tiempo entre balas, el juego no esta pausado y que no se encuentre el mause encima de ningun objeto de la UI
        {
            Disparar ();//metodo disparar
        }

        if(timer >= tiempoEntreBalas * efectosVisualizacion)//si el contador de tiempo es mayor o igual al tiempo entre balas por el tiempo de los efectos de visulaizacion
        {
            DeshabilitarEfectos ();//Metodo desabilitar efectos
        }
    }


    public void DeshabilitarEfectos ()//Metodo que deshabilita dos componentes
    {
        lineaDisparo.enabled = false;
        luzArma.enabled = false;
    }


    void Disparar ()
    {
        if (nivelBalas <= 0)//si el nivel de balas es menor o igula a 0
            return;//salgo del metodo ya qu eno puedo disparar

        nivelBalas--;//disminuyyo la cantidad de balas en 1
        municionSlider.value = nivelBalas;//actualizo el slider por cada bala perdida

        timer = 0f;//el contador de tiempo se reinicia a 0

        audioArma.Play ();//se reproduce el audio del arma

        luzArma.enabled = true;//se habilita al luz del arma

        particulasArma.Stop ();//se detienen y reinicia el clip de las particulas del arma
        particulasArma.Play ();//se reporduce el efecto particulas

        lineaDisparo.enabled = true;//se habilita el componente  lineRenderer
        lineaDisparo.SetPosition (0, transform.position);//se posiciona el primer extremo (0)  del componente Line renderer en la misma posicion en la que se encuentra este objeto 

        disparoRayo.origin = transform.position;//posicionamos el rayo en este objeto
        disparoRayo.direction = transform.forward;//la direccion del rayo saldra en el eje z ó forward

        if(Physics.Raycast (disparoRayo, out objetivoRayo, rango, mascaraDisparable))//Se verifica si: (simulacion raycast (hay un rayo instanciado, resultado de objeto colisionado,distancia maxima de rayo si no encuentra nada devolvera falso, solo colisionara con los objetos que pertenezcan a una capa)
        {
            VidaEnemigo vidaEnemigo = objetivoRayo.collider.GetComponent <VidaEnemigo> ();//accedo al componente enemy Helt del objetivo colisionado
            if(vidaEnemigo != null)//si contiene el objetivo ese componente
            {
                vidaEnemigo.recibirDaño(dañoPorDisparo, objetivoRayo.point);//hace daño al objetivo
                imagenEnemigo.enabled = true;//habilito el componente imagen
                imagenEnemigo.sprite = vidaEnemigo.Icono;//le agrego en su elemnto sprite la imagen de cada enemigo respectivo en su prefab
                vidaSliderEnemigo.gameObject.SetActive(true);//activo el objeto Slider 
                vidaSliderEnemigo.value = vidaEnemigo.TomarPorcentajeVida();//actualizo el slider con el nivel de vida de el enemigo
                StopCoroutine("EsconderInterfazEnemigo");//detengo la corrutina
                StartCoroutine("EsconderInterfazEnemigo");//reinicio la corrutina 
            }
            else
            {
                EsconderObjetosUI();//Ejecuto el metodo
            }
            lineaDisparo.SetPosition (1, objetivoRayo.point);//se posiciona el segundo extremo (1)  del componente Line renderer en la direccion del objetivo colisionado
        }
        else//Si no colisiona con nadie
        {
            lineaDisparo.SetPosition (1, disparoRayo.origin + disparoRayo.direction * rango);//se posiciona el segundo extremo (1)  del componente Line renderer en la direccion del rayo hasta el rango maximo alcanzado
        }
    }

    //Metodo: Corrutina para que se recargue constantemente la municion
    IEnumerator RecargarMunicion()
    {
        float tiempoDeRecarga = tiempoEntreBalas * 2;//el tiempo de recarga va ser el doble del tiempo entre las balas
        while (true)//bucle infinito
        {
            yield return new WaitForSeconds(tiempoDeRecarga);//esperamos el tiempo de recarga
            if (nivelBalas < balasMaximas)//si el nivel de balas es menor a las balas maximas
            {
                nivelBalas++;//aumentamos la cantidad de balas en 1
                municionSlider.value = nivelBalas;//actualizamos el slider con la cantidad de balas
            }
        }
    }

    //Metodo corrutina el cual espera 2 segundo para borrar la interfaz del enemigo
    IEnumerator EsconderInterfazEnemigo()
    {
        yield return new WaitForSeconds(2f);
        EsconderObjetosUI();
    }

    void EsconderObjetosUI()
    {
        imagenEnemigo.enabled = false;//deshabilito el componente imagen
        vidaSliderEnemigo.gameObject.SetActive(false);//desactivo el objeto Slider 
    }
}
