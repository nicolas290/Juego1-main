using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallScript : MonoBehaviour
{
    private Rigidbody2D rb;//rigidbody
    private GameObject[] target;
    public float moveSpeed = 3;//velocidad 
    public Vector2 moveDirection;//direccion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectsWithTag("Player");// el target es el player
        moveDirection = (target[0].transform.position - transform.position).normalized * moveSpeed;//esto lo que hace es mandar las bolas de fuego a la posicion inicial del player
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);//para darle la velocidad
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))//se destruye si colisiona con cualquiera de estos
        {
            Destroy(gameObject);
        }
    }
}
