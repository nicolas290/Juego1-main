using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Mathematics;
using System.IO;
using System;

public class playerController : MonoBehaviour
{
    private Vector2 moveInput;// esta variable ayuda a calcular los movimientos del personaje hacia todos los lados
    public float moveSpeed;//esta variable es para la velocidad de el movimiento
    public Rigidbody2D rb2d;//este es el rigidbody que va a estar conectado al sprite del player, para darle fisica convincente al player
    public Animator playerAnim;//esta es para llamar a las animaciones que se han hecho

    public int direction;//se daran valores numericos a la direccion a la que el persoanje podra ir esta variable es para esto

    public float attackingCoolDown;//variable de cooldown para darle un tiempo de espera entre sus ataques al jugador

    public GameObject espada;//para llamar cosas relacionadas con la espada
    public GameObject arco;// para llamar cosas relacionadas con el arco
    public int arrowCount;//para tener el numero de flechas
    public GameObject arrowPrefab;//el gameobject de las flechas en si
    public int weaponInUse;//para tener un valor numerico que podemos usar para almacenar las distintas armas que se pueden usar

    public bool hurting;//para establecer con un boleano si el jugador esta en estado herido o no
    public GameObject playerSprite;//Para representar al jugador, y asociar a este cosas como que esta recibiendo daño etc...
    public bool stillInEnemyRange;//para calcular si el jugador esta siendo dañado o no

    public int playerHealth;//para el conteo de vida
    public Animator gameOver;//para usar animacion de muerte
    public GameObject heart1;//los diferentes corazones que usaremos para representar vida
    public GameObject heart2;
    public GameObject heart3;

    public TextMeshProUGUI inGameCoinText;//texto para monedas
    public int cointCount;// para hacer el conteo de monedas, este numero se transferira a string para la variable de arriba

    public TextMeshProUGUI inGameHealthPotionText;//Lo mismo que hacen las dos de arriba pero para las pociones de vida
    public int healthPotionCount;

    public TextMeshProUGUI inGameArrowText;// lo mismo para las flechas, la variable de conteo de flechas esta mas arriba en arrowCount

    public GameObject shopButtons;//el gameobject para prender y apagar los botones de la tienda
    private bool isGameOver = false;//para controlar cosas en el estado de game over

    void Start()
    {
        playerHealth = 3;//en start la unica variable que se usara es la del hp del player ya que el juego debe empezar con su vida completa

    }

    // Update is called once per frame
    void Update()
    {
        ///////////////// Comienzo de void update
        ///
        if (isGameOver && Input.GetKeyDown(KeyCode.R)) //Nuevo:reiniciar solo si está en estado Game Over y se presiona "R"
        {
            ResetProgress();
            return; //Salir de Update para evitar que se ejecute más código
        }



        //if para el ataque
        if (attackingCoolDown <= 0 && playerHealth > 0)//si el ataque es menor o igual a 0 y la vida del jugador es mayor a 0. en resumen si el ataque del jugador no esta occuriendo y la vida del jugador es mayor a 0 entonces ocurrirar las animaciones dentro de este if
        {
            rb2d.constraints = RigidbodyConstraints2D.None;// lo que estos dos rigidbody estan haciendo es congelar la rotacion de el player
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;// esto no impide que el jugador se mueve hacia los lados, es para que su valor z no cambie y no este rotando mientras se mueve


            //movimiento
            moveInput.x = Input.GetAxisRaw("Horizontal");//llamando a movimiento para izquierda y derecha
            moveInput.y = Input.GetAxisRaw("Vertical");//llamando movimiento arriba y abajo. Esto es algo implementado en unity para dar movimiento WASD
            moveInput.Normalize();// esto ayuda a normalizar el numero del movimiento para que nos de numeros exactos y no floats

            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))//este if es para que el player se mueva solo arriba, abajo, izquierda, derecha , y no hacia los lados
            {
                moveInput.y = 0;
                rb2d.velocity = moveInput * moveSpeed;//basicamente lo que pasa en este if y else, es que con el mathf, se tiene un seguimiento del x,este se considera mayor que y, y como tiene esa prioridad por haber sido presionado primero, si presionas la teclar hacia arriba el movimiento hacia los lados aun seguira ocurriendo
            }
            else
            {
                moveInput.x = 0;
                rb2d.velocity = moveInput * moveSpeed;//lo mismo pero si se presiona w primero este tiene prioridad y si lo presionas junto a las teclas de izquierda y derecha este seguira teniendo prioridad como direccion
            }

            //if para que animacion ocurrira
            if (moveInput.y < 0)//si el valor del input es menor a 0,ocurrira la animacion de caminar hacia abajo
            {
                playerAnim.Play("playerWalkDown");
                direction = 0;
            }
            else if (moveInput.x > 0)//el valor de x es mayor a 0 animacion caminando izquierda
            {
                playerAnim.Play("playerWalkRight");
                direction = 1;
            }
            else if (moveInput.x < 0)//el valor de x es menor a 0 animacion caminando derecha
            {
                playerAnim.Play("playerWalkLeft");
                direction = 2;
            }
            else if (moveInput.y > 0)//el valor de y es mayor a 0 animacion caminando hacia arriba
            {
                playerAnim.Play("playerWalkUp");
                direction = 3;
            }




            //if para las animaciones del player estando quieto
            if (moveInput.y == 0 && moveInput.x == 0)// es lo mismo de arriba pero para las animaciones quieto
            {
                if (direction == 0)
                {
                    playerAnim.Play("playerIdleDown");
                }
                if (direction == 1)
                {
                    playerAnim.Play("playerIdleRight");
                }
                if (direction == 2)
                {
                    playerAnim.Play("playerIdleLeft");
                }
                if (direction == 3)
                {
                    playerAnim.Play("playerIdleUp");
                }

            }

            //if para el ataque
            if (Input.GetKeyDown(KeyCode.Space))//si se presiona la tecla espacio occurren las acciones abajo
            {
                if (direction == 0)
                {
                    playerAnim.Play("playerAttackD");//ataque hacia abajo
                    attackingCoolDown = 0.4f;//cooldown para ataque
                }
                if (direction == 1)
                {
                    playerAnim.Play("playerAttackR");//hacia la derecha
                    attackingCoolDown = 0.4f;//cooldown
                }
                if (direction == 2)
                {
                    playerAnim.Play("playerAttackL");//hacia la izquierda
                    attackingCoolDown = 0.4f;//cooldown
                }
                if (direction == 3)
                {
                    playerAnim.Play("playerAttackUp");//hacia arriba
                    attackingCoolDown = 0.4f;//cooldown
                }

                //dentro del mismo if elegir si el ataque es disparar flechas
                if (arrowCount > 0 && weaponInUse == 1)//las siguientes acciones occurriran si el contador de flechas es mayor a 0 y si tambien el arma en uso es la 1
                {
                    PlayerPrefs.SetInt("ArrowCount", arrowCount - 1);//estoy chato aaaaaaa aca se esta guardando el valor de las flechas, se resta -1 cada vez que se realiza un ataque
                    if (direction == 0)
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));//el trasnform position es para que la flecha salga de la direccion del player, la segunda parte del codigo es para que salga en direccion hacia abajo
                    }
                    if (direction == 1)
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));//este es default en 0 porque la flecha en default esta mirando hacia la derecha y no necesita ser rotada
                    }
                    if (direction == 2)
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));//hacia izquierda
                    }
                    if (direction == 3)
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));//hacia arriba
                    }
                }
            }




        }
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;//para que el jugador deje de moverse si esta atacando o esta sin vida
        }

        //cambiando armas
        if (Input.GetKey(KeyCode.Alpha1))//alpha 1 es numero 1 en el teclado.Cuando se presiona sera verdadero que cambie a espada
        {
            espada.SetActive(true);
            arco.SetActive(false);
            weaponInUse = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))//letra 2 en el teclado si es verdadero que se presiona se cambiara a arco
        {
            espada.SetActive(false);
            arco.SetActive(true);
            weaponInUse = 1;
        }


        //uso de pociones!!!// actualizado listo para su uso :)//
        if (Input.GetKeyDown(KeyCode.H))//H en el teclado para usar pociones si el contador de estas es mayor a 0 y solo se podran usar si la vida del jugador es menor a 3
            if (healthPotionCount > 0 && playerHealth < 3)
            {
                // Incrementa la vida del jugador
                playerHealth++; // Aumenta 1 punto de vida

                // Reduce la cantidad de pociones y guarda el nuevo valor en PlayerPrefs
                healthPotionCount--;
                PlayerPrefs.SetInt("HealthPotionCount", healthPotionCount);

                // Actualiza el texto de la interfaz con el nuevo contador de pociones
                inGameHealthPotionText.text = healthPotionCount.ToString();

                Debug.Log("Poción usada. Vida actual: " + playerHealth + ", Pociones restantes: " + healthPotionCount);
            }
            else
            {
                Debug.Log("No se puede usar la poción: no hay suficientes pociones o la vida ya está completa.");
            }

        //aqui se calcula el cooldown de los ataques
        if (attackingCoolDown > 0)//si el cooldown del ataque es mayor que 0
        {
            attackingCoolDown -= Time.deltaTime;// este es un contador que occure si empieza la condicion de arriba, es alrededor de un segundo de cooldown
        }

        //perdiendo vida
        if (playerHealth == 3)//si el contador de vida del jugar es igual a 3 los 3 corazones estaran seran true
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        if (playerHealth == 2)// si el contador es de 2 en su vida solo dos corazones seran true
        {
            heart1.SetActive(false);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        if (playerHealth == 1)//si el contador es uno solo 1 corazon sera true
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(true);
        }
        if (playerHealth <= 0)//si el contador de vida es 0 todos los corazones seran falsos
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            gameOver.Play("gameOverAnim");//occurrira animacion de gameover
            isGameOver = true;
            gameObject.GetComponent<Animator>().speed = 0;//esto es para dejar el animador en 0 para que no occurran otras animaciones cuando muera el jugado





        }
        //estas son para mantener seguimiento de conteo de monedas,flechas y pociones
        cointCount = PlayerPrefs.GetInt("CoinCount");
        inGameCoinText.text = cointCount.ToString();//aqui es donde transformamos los int que teniamos a string para mostrarlos como texto en el juego

        arrowCount = PlayerPrefs.GetInt("ArrowCount");
        inGameArrowText.text = arrowCount.ToString();

        healthPotionCount = PlayerPrefs.GetInt("HealthPotionCount");
        inGameHealthPotionText.text = healthPotionCount.ToString();




        //////////////// Fin del void update


    }

    private void Update1()
    {
        // Verifica si se presiona la tecla 'P' y si el jugador tiene pociones disponibles
        if (Input.GetKeyDown(KeyCode.P) && healthPotionCount > 0 && playerHealth < 3)
        {
            UseHealthPotion();
        }

        // Resto de tu lógica de Update...
        if (isGameOver && Input.GetKeyDown(KeyCode.R)) //Nuevo: reiniciar solo si está en estado Game Over y se presiona "R"
        {
            ResetProgress();
            return; //Salir de Update para evitar que se ejecute más código
        }
    }

    private void UseHealthPotion()
    {
        throw new NotImplementedException();
    }




    //algunas funciones mas, estas se haran fuera del void update
    public void OnTriggerEnter2D(Collider2D collision)//esto es para detectar colisiones 2d o cosas que vayan dentro del player
    {
        if (collision.gameObject.CompareTag("Heart") && playerHealth < 3)//con esto se detecta el item que esta entrando en el player, si el hp es menor a 3 ocurrira
        {
            playerHealth++;//se agregara 1 punto
            Destroy(collision.gameObject);//y se destruira el objeto
        }
        if (collision.gameObject.CompareTag("Coin"))//si se encuentran monedas
        {
            PlayerPrefs.SetInt("CoinCount", cointCount + 1);//se sumaran
            Destroy(collision.gameObject);//y se destruira el objeto
        }
        if (collision.gameObject.CompareTag("Shop"))//si estamos dentro del area de tienda
        {
            shopButtons.SetActive(true);//se activaran los botones
        }
    }

    public void OnTriggerExit2D(Collider2D collision)//un trigger por si se sale de la tienda
    {
        if (collision.gameObject.CompareTag("Shop"))//cuando salgamos de la tienda
        {
            shopButtons.SetActive(false);//los botones desapareceran
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)//para colision con enemigos
    {
        if (collision.gameObject.CompareTag("Enemy") && hurting == false && playerHealth > 0)//hurting en falso para no dañar el player denuevo si ya esta siendo herido, ademas de establecer que tiene que tener mas de 0 hp para poder ser herido por colision de enemigos
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.red;//para que el sprite del jugador cambie a rojo si es herido
            playerHealth--;//-1 de vida
            StartCoroutine(whitecolor());//inicializar color blanco si no esta siendo herido
            if (playerHealth < 0)//si la vida del jugador es mayor a 0
            {
                transform.position = Vector2.MoveTowards(transform.position, collision.gameObject.transform.position, -70 * Time.deltaTime);//aqui ocurre un calculo para hacer knockback, o lanzar al jugador hacia atras si es dañado
            }
            hurting = true;//el jugador esta siendo dañado
        }
    }

    IEnumerator whitecolor()//para cambiar a color blanco cuando se deje de dañar al jugador
    {
        yield return new WaitForSeconds(2);//se esperara un par de segundos antes de que ocurra esto para hacer la transicion mas fluida
        if (playerHealth > 0)
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.white;//solo mandar el color blanco si el jugador aun tiene mas de 0 hp
        }
        hurting = false;//despues de los dos segundos el hurting cambiara a falso, o sea el estado de herido dejara de ocurrir
        GetComponent<BoxCollider2D>().enabled = false;//desactivando y activando la colision, sino puede ocurrir un bug donde no se renicia la colision se choco con algo y luego vuelvo a chocar con ello
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void playAgain()//boton para jugar denuevo
    {
        SceneManager.LoadScene(0);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void buyArrow()//boton de comprar flecha
    {
        if (cointCount >= 5)//si no tenemos 5 monedas no se puede
        {
            PlayerPrefs.SetInt("CoinCount", cointCount - 5);//perdemos las 5 monedas
            PlayerPrefs.SetInt("ArrowCount", arrowCount + 1);//ganamos una flecha
        }
    }

    public void buyHealthPotion()//boton de comprar pocion
    {
        if (cointCount >= 5)//si no tenemos 5 monedas no se puede
        {
            PlayerPrefs.SetInt("CoinCount", cointCount - 5);//perdemos las 5 monedas
            PlayerPrefs.SetInt("HealthPotionCount", healthPotionCount + 1);//ganamos una pocion
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); // Elimina todos los datos guardados en PlayerPrefs
        PlayerPrefs.Save(); // Guarda los cambios
        Debug.Log("Progreso del juego reiniciado.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
    }
}