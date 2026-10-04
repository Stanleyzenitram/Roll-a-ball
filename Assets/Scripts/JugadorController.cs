using UnityEngine;
using TMPro;

public class JugadorController : MonoBehaviour
{
    //Declaro la variable de tipo Rigidbody que luego asocio al jugador
    private Rigidbody rb;

    public float velocidad;

    private int contador;

    public TMP_Text textoContador, textoGanar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;

        //actualizo el texto del contador por primera vez
        setTextoContador();

        //inicio el text de ganar en blanco
        textoGanar.text = "";
    }

    // Para que se sincronice con los frames de física del motor
    void FixedUpdate()
    {
        //Estas variables nos capturan el movimiento en horizontal y vertical de nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
        //Un vector 3 es un trío de posiciones en el espacio XYZ, en este caso el que corresponde al movimiento
        Vector3 movimiento = new Vector3(movimientoH, 0.0f,
        movimientoV);
        //Asigno ese movimiento o desplazamiento a mi RigidBody
        rb.AddForce(movimiento * velocidad);
    }

    //se ejecuta al entrar a un objeto con la opcion isTrigger
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coleccionable"))
        {
            //desactivo el obj
            other.gameObject.SetActive(false);

            //incremento el contador de objetos recogidos
            contador++;
            //actualizo el texto del contador
            setTextoContador();
        }
    }
    

    void setTextoContador()
    {
        textoContador.text = "Contador: " + contador.ToString();
        if(contador >= 12)
        {
            textoGanar.text = "¡Ganaste!";
        }
    }

}
