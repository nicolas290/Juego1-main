using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class snakeScript : MonoBehaviour
{
    private float latestDirectionChangeTime;//para cambiar la direccion en la que se mueve el enemigo basada en tiempo
    private readonly float directionChangeTime = 1.5f;// la velocidad del cambio en si
    private float characterVelocity = 2f;//velocidad del personaje
    private Vector2 movementDirection;//vector para la direccion
    private Vector2 movementPerSecond;//vector movimiento por segundo todos estos son para darle movimiento random al enemigo y que no solo siga al player

    public int health;//vida de enemigo
    public GameObject droppedItems;//item que droppea
    void Start()
    {
        latestDirectionChangeTime = 0f;//para inicializar el cambio de direccion
        calculateNewMovementVector();//para calcular esa nueva direccion
    }

    void calculateNewMovementVector()
    {
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;//darle movimiento random tanto a la x como la y
        movementPerSecond = movementDirection * characterVelocity;//para que se mueva el enemigo basado en la velocidad que le demos
    }
    

    
    void Update()
    {
        if (health > 0)// si la salud es mayor a 0
        {
            if(Time.deltaTime - latestDirectionChangeTime > directionChangeTime)// esto es basicamente un timer para el cambio de direccion
            {
                latestDirectionChangeTime = Time.deltaTime;//se haria el calculo de movimiento
                calculateNewMovementVector();
            }
            if (MathF.Abs(movementPerSecond.x) > MathF.Abs(movementPerSecond.y))//calculo de la direccion en x, si el movimiento de x es mayor a y
            {
                movementPerSecond.y = 0;//se cancela movimiento  y
                transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),//toda esta es la funciona que lo mueve
                    transform.position.y + (movementPerSecond.y * Time.deltaTime));
            }
            else//lo mismo pero para el movimeito de x
            {
                movementPerSecond.x = 0;
                transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
                    transform.position.y + (movementPerSecond.y * Time.deltaTime));
            }
        }
        else
        {
            Instantiate(droppedItems, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));//drop de item
            gameObject.SetActive(false);//enemigo desaparece
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//mismo codigo de otros enemigos no quiero explicarlo
    {
        if (collision.gameObject.CompareTag("Espada1"))
        {
            health--;
            calculateNewMovementVector();
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor());
        }
        if (collision.gameObject.CompareTag("Arrow1"))
        {
            health--;
            calculateNewMovementVector();
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor());
        }

    }

    IEnumerator whitecolor()//color blanco 
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color= Color.white;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;

    }
}
