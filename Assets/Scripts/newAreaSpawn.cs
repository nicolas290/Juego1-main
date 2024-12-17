using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newAreaSpawn : MonoBehaviour
{
    public GameObject spawnPoint;//para spawnear personajes
    public GameObject previousScene;//cuando vayamos a una nueva escena esto servira para apagar la anterior
    public GameObject newScene;//para prender la nueva
    public GameObject mainCamera;//para transportar la camara al nuevo escenario

    public Animator sceneChangeAnim;//para usar la animacion de cambio de escena que cree
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)//al colisionar con objeto 2d
    {
        if (other.gameObject.CompareTag("Player"))//se compara tags con el player, para que esto ocurra solo cuando el player colisione con este
        {
            sceneChangeAnim.Play("switchScene");//que se active la animacion de cambio de escena
            mainCamera.transform.position = spawnPoint.transform.position + new Vector3(0, 0, -10);//con el mainCamera transform estamos cambiando la posicion de la camara hacia el nuevo spawn.El new vector 3 con -10 es debido a que la camara tiene un valor default de -10
            other.transform.position=spawnPoint.transform.position;//esta parte movera al player el other representa cualquier cosa que se este llamando en el tag, o sea Player
            previousScene.SetActive(false);//se apaga la scena anterior
            newScene.SetActive(true);// se activa la nueva
        }
    }
}
