using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcScript : MonoBehaviour
{
    public GameObject npcConversation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)//entrando en coversacion
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            npcConversation.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)//saliendo
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            npcConversation.SetActive(false);
        }
    }
}
