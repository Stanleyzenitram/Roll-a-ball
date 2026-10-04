using UnityEngine;

public class Rotador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rota el elemento una cant diferente en cada direccion y en cada intervalo de tiempo
        transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
    }
}
