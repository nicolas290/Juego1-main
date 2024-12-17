using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointControllerScript : MonoBehaviour
{
    public GameObject checkPointOne;//primer checkpoint
    public GameObject checkPointTwo;//segundo check

    public GameObject mainCamera;//camara
    public GameObject player;//jugador

    public GameObject sceneOneItems;
    public GameObject sceneTwoItems;
    void Start()//el codigo estara en el start porque solo queremos que se inicien los checkpoints cuando comienze el juego
    {
        if (PlayerPrefs.GetInt("checkPoint") == 1)//Si el valor iguala a 1 se activara el primer checkpoint
        {


            mainCamera.transform.position = checkPointOne.transform.position + new Vector3(0, 0, -10);
            player.transform.position = checkPointOne.transform.position;
            sceneOneItems.SetActive(true);
            sceneTwoItems.SetActive(false);//se activara una escena y desactivara la anterior
        }
        if (PlayerPrefs.GetInt("checkPoint") == 2)//Si el valor iguala a 1 se activara el segundo checkpoint
        {


            mainCamera.transform.position = checkPointTwo.transform.position + new Vector3(0, 0, -10);
            player.transform.position = checkPointTwo.transform.position;
            sceneOneItems.SetActive(false);
            sceneTwoItems.SetActive(true);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
