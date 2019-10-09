using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int saludInicial = 100;//salud con la que va iniciar el Personaje
    public int nivelSalud;//nivel de salud que varia con las diferentes interacciones
    public float velocidadHundimiento = 2.5f;//velociada con la cual el enemigo se ira hundiendo en el suelo
    public int valorPuntuacion = 10;//valor de la puntuacion que dara el enemigo al ser destruido
    public AudioClip clipMuerte;//variable publica donde ira el clip de audio Muerte
    public Sprite Icono;// variable que contendra la referencia al componente(Sprite) la cual se modificara por cada enmigo que se le dispare

    Animator anim;//variable para alojar una animacion
    AudioSource audioEnemigo;//variable paar aalojar el componente audio source
    ParticleSystem golpeParticulas;//variable para alojar componente particulas
    CapsuleCollider capsuleCollider;//variable que va alojar la capsula de colisiones
    bool estaMuerto;//variable para confirmar la Muerte del enemigo
    bool estaHundido;//variable para confirmar si el enemigo ya ha desapareciodo del eescenario


    void Awake ()
    {
        anim = GetComponent <Animator> ();//referenciando el animator del este objeto
        audioEnemigo = GetComponent <AudioSource> ();//referenaciado el audio source de este objeto
        golpeParticulas = GetComponentInChildren <ParticleSystem> ();// referenciando el objeto hijo que contiene un componente ParticleSystem
        capsuleCollider = GetComponent <CapsuleCollider> ();//referenciado el componente capsule colider de este objeto

        nivelSalud = saludInicial;//al iniciar la escena el nivel de salud será el por defecto(100)
    }

    //Metodo que toma el porcentaje de la vida y no le numero
    public int TomarPorcentajeVida()
    {
        return 100 * nivelSalud / saludInicial;
    }

    void Update ()
    {
        if(estaHundido)//Si esta hundido
        {
            transform.Translate (-Vector3.up * velocidadHundimiento * Time.deltaTime);//se hundira poco a poco
        }
    }


    public void recibirDaño (int cantidad, Vector3 golpePunto)//Metodo publico que se utiliza cuando el enemgo recibe daño
    {
        if(estaMuerto)//si esta muerto
            return;//sale

        audioEnemigo.Play ();//reproduce el audio del enemigo

        nivelSalud -= cantidad;//se ira quitando salud al enemigo con el valor del golpe 
            
        golpeParticulas.transform.position = golpePunto;//la posicion del componente particulas se ubicara en el punto del golpe
        golpeParticulas.Play();//reproduce animacion de particulas

        if(nivelSalud <= 0)//si el nivel de salud es menor o igual a 0
        {
            Muerte ();//Metodo muerte
        }
    }


    void Muerte ()//metodo que activa toda la iteracion de la muerte
    {
        estaMuerto = true;//este pasa a ser verdadero

        capsuleCollider.isTrigger = true;//activa el elmento trigger del componte componente collider para que atravise

        anim.SetTrigger ("Muerte");//ejecuto la animacion teniendo en cuenta el trigger de la transicion

        audioEnemigo.clip = clipMuerte;//cambio el elmento clip por el de muerte
        audioEnemigo.Play ();// lo repuduzco
    }


    public void IniciarHundimiento()//proceso en el cual el objeto enemigo atravesará el piso (se ejecuta en un evento del Modelo en la animacion Muerte del Enemigo)
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;//desactivo el NavmeshAgent de mi enemigo y lo desactivo
        GetComponent <Rigidbody> ().isKinematic = true;//Activo el elmeto is kinematic para que este no pueda ser movido or ninguna fuerza externa
        estaHundido = true;//esta hundido pasa a ser verdadero
        AdministrarPuntuacion.puntuacion += valorPuntuacion;//se le añadira el valor de la puntuacion del enemigo al marcador
        Destroy (gameObject, 2f);//destruyo el gameobject dentro de 2 segundos
    }
}
