using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public float speed = 3.5f;//velocidad de flecha
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation== Quaternion.Euler(new Vector3(0, 0, 90)))//flecha hacia abajo
        {
            transform.Translate((transform.up * -speed * Time.deltaTime));
        }
        if (transform.rotation == Quaternion.Euler(new Vector3(0, 0, -90)))//flecha hacia arriba 
        {
            transform.Translate((transform.up * speed * Time.deltaTime));
        }
        if (transform.rotation == Quaternion.Euler(new Vector3(0, 0, 0)))// flecha hacia la derecha
        {
            transform.Translate((transform.right * speed * Time.deltaTime));
        }
        if (transform.rotation == Quaternion.Euler(new Vector3(0, 0, -180)))//flecha hacia la izquierda
        {
            transform.Translate((transform.right * -speed * Time.deltaTime));
        }

        Destroy(gameObject, 5);//se destruye el objeto flecha despues de 5 segundos
    }

    private void OnTriggerEnter2D(Collider2D collision)// para remover la flecha si toca a un enemigo
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
