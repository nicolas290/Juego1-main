using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public GameObject flameball;//game object de la bola de fuego
    public GameObject target1;//todos los targets
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public float attackingCoolDown;//cooldown de ataque
    public int timeBetweenAttack;//tiempo entre ataques
    public int health;//vida
    public GameObject blockingWall;//para apagar el muro cuando se termine el boss
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)//si la vida del boss es igual a 0
        {
            blockingWall.SetActive(false);//desaparece muralla
            gameObject.SetActive(false);//el gameobject del boss desaparece
            flameball.SetActive(false);
        }

        if (attackingCoolDown > 0)//contador para el cooldown de ataque, similar al de otros enemigos
        {
            attackingCoolDown -= Time.deltaTime;
        }

        else//si no se esta en cooldown
        {
            Instantiate(flameball, target1.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));//se inician las bolas de fuego
            Instantiate(flameball, target2.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Instantiate(flameball, target3.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Instantiate(flameball, target4.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

            attackingCoolDown = timeBetweenAttack;//se resetea

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//todo esto es solo para que reciba daño, lo mismo que otros enemigos
    {
        if (collision.gameObject.CompareTag("Espada1"))
        {
            health--;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor());
        }
        if (collision.gameObject.CompareTag("Arrow1"))
        {
            health--;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor());
        }
    }

    IEnumerator whitecolor()//deja de estar rojo despues de unos segundos
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color= Color.white;
    }
}
