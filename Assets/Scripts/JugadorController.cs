using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class JugadorController : MonoBehaviour
{
    //Declaro la variable de tipo Rigidbody que luego asocio al jugador
    private Rigidbody rb;

    public float velocidad;

    private int contador;

    public TMP_Text textoContador, textoGanar;

    private NivelController nivelController; //refencia al script NivelController

    private int totalColeccionables;

    public int tiempoAumentar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;

        //actualizo el texto del contador por primera vez
        setTextoContador();

        //inicio el text de ganar en blanco
        textoGanar.text = "";

        nivelController = FindAnyObjectByType<NivelController>(); //busco el script NivelController en la escena
        tiempoAumentar = 0; //tiempo que se aumentará al recoger un coleccionable

        totalColeccionables = GameObject.FindGameObjectsWithTag("Coleccionable").Length; //cuento la cantidad de coleccionables en la escena
        Debug.Log("Total de coleccionables en la escena: " + totalColeccionables);
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

            if(nivelController != null)
            {
                //Aumento el tiempo del nivel en 2 segundos
                nivelController.AumentarTiempo(2f);
            }
        }
    }
    

    void setTextoContador()
    {
        textoContador.text = "Contador: " + contador.ToString();
        if(contador >= totalColeccionables && contador > 0) //si el contador es mayor o igual a la cantidad de coleccionables y es mayor a 0
        {
            textoGanar.text = "¡Ganaste!";
            textoGanar.color = Color.green;
            StartCoroutine(MostrarVictoria());

        }
    }

       IEnumerator MostrarVictoria()
    {
        // Pausar el juego
        Time.timeScale = 0f;

        // Esperar 5 segundos en tiempo real
        yield return new WaitForSecondsRealtime(5);

        // Restaurar el tiempo y volver al menú principal
        Time.timeScale = 1f;

        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int ultimaEscena = SceneManager.sceneCountInBuildSettings -1;
        if (escenaActual < ultimaEscena)
        {
            SceneManager.LoadScene(escenaActual + 1);
        }
        else
        {
            // Ya estás en el último nivel → volver al menú principal
            SceneManager.LoadScene("MenuPrincipal");
        }
    }

}
