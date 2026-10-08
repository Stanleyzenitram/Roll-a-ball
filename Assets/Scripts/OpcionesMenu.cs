using UnityEngine;

public class OpcionesMenu : MonoBehaviour
{
    public void Volver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuPrincipal");
    }
}
