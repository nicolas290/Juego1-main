using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTriggerScript : MonoBehaviour
{
    public GameObject wall;//para activar muro
    public GameObject boss;//para activar boss
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            wall.SetActive(true);
            boss.SetActive(true);
        }
    }
}
