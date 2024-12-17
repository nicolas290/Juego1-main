using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class checkPointScript : MonoBehaviour
{
    public int checkPoinNumber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)//cuando se colisiona 
    {
        if (collision.CompareTag("Player"))//con el player bla bla bla
        {
            if (checkPoinNumber == 1)
            {
                PlayerPrefs.SetInt("checkPoint", 1);

            }
            if (checkPoinNumber == 2)
            {
                PlayerPrefs.SetInt("checkPoint", 2);

            }
        }
    }
}
