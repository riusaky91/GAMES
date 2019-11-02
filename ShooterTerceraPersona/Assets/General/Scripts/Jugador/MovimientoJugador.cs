using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 6f;//velocidad de movimeinto

    Vector3 movimiento;//hacia donde se quiere avanzar
    Animator anim;//referencia al componente animator
    Rigidbody rb;//referencia al componente rigidbody
    int MascaraPiso;
    float longitudRayo = 100f;//Longitud entre la camara y el piso
    Joystick joystick;//referencia al joystick 

    private void Awake()
    {
        anim = GetComponent<Animator>();//obteniendo referencia
        rb = GetComponent<Rigidbody>();//Obteniendo referencia
        MascaraPiso = LayerMask.GetMask("Floor");//asigno la capa a la variable
        joystick = Joystick.FindObjectOfType<Joystick>();

    }

    private void FixedUpdate()//cada ciclo del motor de fisicas
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");//Obteniendo -1/0/1 dependiendo de lo que se oprima derecha izquierda
        //float vertical = Input.GetAxisRaw("Vertical");////Obteniendo -1/0/1 dependiendo de lo que se oprima arriba y abajo
        float horizontal = joystick.Horizontal;//Obteniendo -1/0/1 dependiendo de lo que se mueva el joystick
        float vertical = joystick.Vertical;////Obteniendo -1/0/1 dependiendo de lo que se se mueva el joystick

        Mover(horizontal, vertical);
        Girar();
        Animando(horizontal, vertical);
    }

    void Mover(float horizontal, float vertical)//Metodo que mueve el personaje recibiendo los axis
    {
        movimiento.Set(horizontal, 0f, vertical);//establesco valores para el vector
        movimiento = movimiento.normalized * velocidad * Time.deltaTime;//el vector siempre va tener una longitud de uno incluso en diagonal y se multiplica con la velocidad y el tiempo
        rb.MovePosition(transform.position + movimiento);//se utiliza para mover el objeto desde el rigidbody(posicion actual+movimiento dado);

    }

    void Girar()
    {
        Ray rayoCamara = Camera.main.ScreenPointToRay(Input.mousePosition);//Obtiene una posicion en este caso la posicion sera dada por el puntero del mouse

        RaycastHit golpeSuelo;
        if (Physics.Raycast(rayoCamara, out golpeSuelo, longitudRayo, MascaraPiso))//Devuelve verdadero solo si ha colisionado con algo
        {
            Vector3 mouseAJugador = golpeSuelo.point - transform.position; //vector que hay entre el personaje y el pulsamiento del raton
            mouseAJugador.y = 0f;//no se debe rotar en un eje

            Quaternion nuevaRotacion = Quaternion.LookRotation(mouseAJugador);
            rb.MoveRotation(nuevaRotacion);//Se implementa la rotacion al personaje
        }
    }



    void Animando(float horizontal, float vertical)//metodo para activar una animacion
    {
        bool caminando = !((vertical == 0) && (horizontal == 0));//no estara caminando cuando vertical y horizontal estan en 0
        anim.SetBool("EstaCaminando", caminando);//activo la transición con el parametro boolenao en true
    }
}
