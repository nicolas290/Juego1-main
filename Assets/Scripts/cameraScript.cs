using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject objectToFollow;//variable que seguira la camara(el player)
    public float speed = 3.0f;//velocidad a la que lo sigue
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float interpolation=speed*Time.deltaTime;

        Vector3 position=this.transform.position;//Calculo del seguimiento de la camara 
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);//aqui se toma al y y el objecttofollow que es el player
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);//lo mismo pero para tomar la posicion del player en x

        this.transform.position = position;//basicamente esto codigo hace que la camara siga al player


    }
}
