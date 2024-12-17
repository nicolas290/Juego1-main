using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeScript : MonoBehaviour
{
    public static bool moving;//si el enemigo se esta moviendo o no
    public GameObject Player;//player
    public float speed = 2.0f;//velocidad del slime
    public float knockBackForce;//knock back del slime, fuerza a la que se repele
    public Rigidbody2D rb;//rigidbody que tendra el slime para fisicas
    public int health;//vida

    public float interactRange;//el rango en el que el slime empezara a seguir al jugador
    public bool seenPlayer;//si el player esta siendo visto

    public GameObject droppedItem;//cuando derrotes al slime tirara un objeto
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(Player.transform.position, this.transform.position) < interactRange || seenPlayer == true)//Para calcular si el jugador esta o no cerca del enemigo
        {
            seenPlayer= true;//si eso pasa entonces el jugador siendo visto se queda en true
            if(health > 0)//si el slime aun tiene vida
            {
                moving = true;//moverse sera true
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);//con el movetowards se movera a la posicion del sprite del player a cierta velocidad
            }
            if(health <= 0)//si la vida es menor o igual a 0
            {
                Instantiate(droppedItem, transform.position, Quaternion.Euler(new Vector3(0,0,0)));//se dropeara un item del slime, en esa misma posicion que murio
                gameObject.SetActive(false);//el gameobject del slime se convertira en false y desaparecera
            }
            rb.constraints = RigidbodyConstraints2D.None;// lo que estos dos rigidbody estan haciendo es congelar la rotacion de el player
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Espada1"))//si es la espada
        {
            seenPlayer = true;
            health--;//perdera vida
            if(health > 0)//si su vida es mayor de 0, tendra knockback
            {
                transform.position=Vector2.MoveTowards(transform.position, Player.transform.position, -100 * Time.deltaTime);//este es el calculo del knockback
            }
            gameObject.GetComponent<SpriteRenderer>().color= Color.red;//se hara el objeto, el slime en cuestion rojo
            StartCoroutine(whitecolor());
        }
        if (collision.gameObject.CompareTag("Arrow1"))//lo mismo pero con flechas
        {
            seenPlayer = true;
            health--;
            if (health > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -50 * Time.deltaTime);
            }
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor());
        }

    }
    
    IEnumerator whitecolor()//color blanco
    {
        yield return new WaitForSeconds(0.2f);//se esperaran 0.2 s
        gameObject.GetComponent<SpriteRenderer>().color=Color.white;//hasta que vuelva a blanco
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;

    }

}
