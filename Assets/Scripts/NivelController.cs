using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelController : MonoBehaviour
{
    public float tiempoNivel = 60f; // Tiempo en segundos para completar el nivel
    public TMP_Text txtTiempo;
    public TMP_Text txtMensaje;

    private bool juegoTerminado = false;

    public void AumentarTiempo(float cantidad)
    {
        tiempoNivel += cantidad;
    }

    void Start()
    {
        // Inicializar el texto del temporizador
        txtTiempo.text = "" + Mathf.CeilToInt(tiempoNivel).ToString();
        txtMensaje.text = "";
    }

    void Update()
    {
        // Restar el tiempo transcurrido
        tiempoNivel -= Time.deltaTime;

        // Actualizar el texto del temporizador
        txtTiempo.text = "" + Mathf.Max(0, Mathf.CeilToInt(tiempoNivel)).ToString();

        if (tiempoNivel <= 20f)
        {
            txtTiempo.color = Color.red; // Cambiar el color del texto a rojo
        }
        else
        {
            txtTiempo.color = Color.green; // Mantener el color del texto en blanco
        }

        // Verificar si el tiempo se ha agotado
        if (tiempoNivel <= 0f)
        {
            juegoTerminado = true;
                StartCoroutine(MostrarMensajePerdiste());
        }
    }

        IEnumerator MostrarMensajePerdiste()
    {
        txtMensaje.text = "¡Tiempo agotado, perdiste!";
        txtMensaje.color = Color.red;

        // Pausar el juego
        Time.timeScale = 0f;

        // Esperar 5 segundos en tiempo real (aunque el juego esté pausado)
        yield return new WaitForSecondsRealtime(5);

        // Restaurar el tiempo y cargar el menú principal
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReiniciarNivel()
    {
        // Reiniciar el nivel actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
