using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomScript : MonoBehaviour
{
    public float attackingCoolDown;//variable para el cooldown del ataque, para que los hongos no esten atacando sin parar
    public int timeBetweenAttack;//el delay que hay entre ataques
    public GameObject attackingObjects;//para los hongos que rodean al hongo principal
    public int health;//vida del enemigo
    public GameObject droppedItem;//dropp del enemigo
    void Start()
    {
        attackingCoolDown = timeBetweenAttack;//el coldown de ataque es igual el tiempo que se demora en volver a atacar, para que ataque cada vez que su cooldown se acabe
    }

    // Update is called once per frame
    void Update()
    {
        if (attackingCoolDown > 0)//si el cooldown de ataque es mayor a 0
        {
            attackingCoolDown -= Time.deltaTime;//empezara a bajar en tiempo real
        }
        else//si el contador no esta bajando
        {
            attackingObjects.SetActive(true);//los objetos de ataque en este caso los hongos estaran en true 
            StartCoroutine(attack());//empieza a atacar, esto es para prender o apagar los hongos si atacan
            attackingCoolDown = timeBetweenAttack;//reseteo del cooldown
        }
        if (health <= 0)//si la vida es menor o igual a 0
        {
            Instantiate(droppedItem, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));//droppeara item
            gameObject.SetActive(false);//desaparecera
        }
    }

    IEnumerator attack()//un time
    {
        yield return new WaitForSeconds(0.5f);//esperara esa cantidad de segundos
        attackingObjects.SetActive (false);//y pondra los objetos de ataque en falso despues de ese tiempo
    }

    private void OnTriggerEnter2D(Collider2D collision)//en colision
    {
        if (collision.gameObject.CompareTag("Espada1"))//si el enemigo colisiona con la espada
        {
            health--;//se le restara vida
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;//tomara color rojo
            attackingObjects.SetActive(true);//los objetos de ataque estaran en true
            StartCoroutine (attack());//comenzaran a atacar
            attackingCoolDown = timeBetweenAttack;//se reseteara el cooldown de ataque
            StartCoroutine(whitecolor());//color blanco si no esta siendo atacado
        }
        if (collision.gameObject.CompareTag("Arrow1"))//si el enemigo colisiona con la flecha
        {
            health--;//se le restara vida
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            attackingObjects.SetActive(true);
            StartCoroutine(attack());
            attackingCoolDown = timeBetweenAttack;
            StartCoroutine(whitecolor());
        }
    }

    IEnumerator whitecolor()//para el color blanco o cuando se acabe le contador de estar siendo dañado
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color= Color.white;//volvera a color blanco despues de esos segundos
    }
}
