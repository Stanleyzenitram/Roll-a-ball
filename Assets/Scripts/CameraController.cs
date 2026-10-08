using UnityEngine;

public class CameraController : MonoBehaviour
{

    //referencia al jugador
    public GameObject jugador;

    //para registrar la diff entre la pos de la camara y del jugador
    private Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - jugador.transform.position;
    }


    //se ejecuta cada frame, pero despues de haber proc todo, es mas exacto para la cam
    void LateUpdate()
    {
        //actualizo la pos de la camara
       transform.position = jugador.transform.position + offset;
    }
}
