using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)//si colisiona
    {
        if (collision.gameObject.CompareTag("Player"))//con el player
        {
            PlayerPrefs.SetInt("collectedFruit", 1);//el valo de collectedFruit subira a 1
            gameObject.SetActive(false);//el gameobject sera falso y desaparecera del escenario
        }
    }
}
