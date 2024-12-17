using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questController : MonoBehaviour
{
    public GameObject questoConvo;//conversacion de quest
    public GameObject inQuestConvo;//para continuar las conversacion
    public GameObject iHaveTheFruit;//la conversacion si tengo la fruta
    public GameObject fruitObject;//para ver si tengo la fruta o no
    public GameObject unlockedDoor;//puerta a desbloquear

    public GameObject fruitUi;//fruta
    public GameObject keyUi;//llave
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("collectedFruit") == 1)//si el player tiene una fruta lo que esta dentro del if pasara
        {
            fruitUi.SetActive(true);//se tendra activa la fruta
            keyUi.SetActive(false);//la llave seguira estando inactiva
            unlockedDoor.SetActive(false);//puerta seguira cerrada
            fruitObject.SetActive(false);//la fruta dentro del juego sera inactiva porque tiene que desaparecer del mapa una vez la tengamos

        }
        if (PlayerPrefs.GetInt("keyValue") == 1)//si tenemos la llave estos pasaran
        {
            fruitUi.SetActive(false);//se apagara la fruta dentro de los objetos cuando se la demos
            keyUi.SetActive(true);//se activara la llave porque nos la dara
            unlockedDoor.SetActive(true);//la puerta se desbloquara
            fruitObject.SetActive(false);//la fruta seguira inactiva

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)//colision
    {
        if (collision.gameObject.CompareTag("Player"))//si somos el player
        {
            if (PlayerPrefs.GetInt("keyValue") == 0)//si nuestro valo de la llave es 0
            {
                if (PlayerPrefs.GetInt("questAccepted") == 0)//podemos entrar en la convesacion de aceptar la quest si esta en 0
                {
                    questoConvo.SetActive(true);//conversacion de quest en true
                    inQuestConvo.SetActive(false);//inactiva mientras no la aceptes
                    iHaveTheFruit.SetActive(false);//inactiva mientras no aceptes la quest
                }
                if (PlayerPrefs.GetInt("questAccepted") == 1)//si la quest es aceptada
                {
                    if (PlayerPrefs.GetInt("collectedFruit") == 1)//primera conversacion que se podra tener, si tenemos la fruta
                    {
                        iHaveTheFruit.SetActive(true);//tener la fruta en true
                        questoConvo.SetActive(false);//conversacion de quest en falsa
                        inQuestConvo.SetActive(false);//conversacion de estare en medio de quest falsa
                    }
                    else//si no tengo la fruta
                    {
                        questoConvo.SetActive(false);
                        inQuestConvo.SetActive(true);//solo se tendra en true la conversacion de quest 2
                        iHaveTheFruit.SetActive(false);
                    }
                }
            }
        }
    }

    //botones
    public void noToQuest()//cuando se presione este boton
    {
        PlayerPrefs.SetInt("questAccepted", 0);//aceptar quest estara en 0 porque no estaremos mas en esa quest
        questoConvo.SetActive(false);//todas las conversaciones de quest en false
        inQuestConvo.SetActive(false);
    }

    public void acceptQuest()//si la aceptamos
    {
        PlayerPrefs.SetInt("questAccepted", 1);//quest tendra valor de 1
        questoConvo.SetActive(false);//todas las conversaciones estaran en false
        inQuestConvo.SetActive(false);
        iHaveTheFruit.SetActive(false);
    }
    public void fruitButton()//si tenemos la fruta aparece el boton de fruta
    {
        PlayerPrefs.SetInt("keyValue", 1);//el valor de llave sube a 1 al entregarla
        questoConvo.SetActive(false);//todas las conversaciones estaran en false despues de terminarla
        inQuestConvo.SetActive(false);
        iHaveTheFruit.SetActive(false);
    }
}
