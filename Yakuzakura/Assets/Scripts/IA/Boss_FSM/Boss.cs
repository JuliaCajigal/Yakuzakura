                       using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    //Referencias
    public Animator anim;
    public Animator animInterfaz;
    public GameObject wave;
    public GameObject shuriken;
    public GameObject bomb;
    public GameObject ramen;
    public GameObject player;
    public Player players;
    public CameraShake camShake;

    //Audio
    public AudioSource mySpeaker;
    public AudioClip bossHit;
    public AudioClip stamp;

    System.Random rnd = new System.Random();
    int numRnd;

    //Vida y barra de vida
    public Image healthBar;
    public float maxHealth = 100f;
    public static float sumo_health;
    public float timeDamage;
    private bool firstTime;

    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        mySpeaker = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        sumo_health = 100f;
        player = GameObject.FindGameObjectWithTag("Players");
        players = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Tiempo entre ataques, recibir daño una vez
        timeDamage -= Time.deltaTime;

        //Barra de vida
        healthBar.fillAmount = sumo_health / maxHealth;
    }

    //Ataque FASE 1: lanza shurikens que siguen al jugador
    public void phaseOne()
    {
                Debug.Log("--------------------------------ATAQUE FASE UNO");

        Instantiate(shuriken, transform.position + new Vector3(-3, 0, -0.2f), Quaternion.identity);
        Instantiate(shuriken, transform.position + new Vector3(3, 0, -0.2f), Quaternion.identity);
        StartCoroutine(camShake.Shake(.15f, .8f));


    }

    //Ataque FASE 2: pisotón que causa onda expansiva
    public void phaseTwo()
    {
        Debug.Log("--------------------------------ATAQUE FASE DOS");

        Instantiate(wave, transform.position + new Vector3(0f, -1, -0.2f), Quaternion.identity);
        StartCoroutine(camShake.Shake(.15f, .8f));


    }

    //Ataque FASE 3: bombas con temporizador
    public void phaseThree()
    {
        anim.SetTrigger("Throw");
        Debug.Log("--------------------------------ATAQUE FASE TRES");

        numRnd = rnd.Next(-2,0);


        if(firstTime == true)
        {
            //Ramen
            GameObject ramen1 = Instantiate(ramen, transform.position + new Vector3(-3, numRnd, -0.2f), Quaternion.identity);
            Vector3 distanceRamen1 = (transform.position - ramen1.transform.position);
            Rigidbody2D ramen1rb = ramen1.GetComponent<Rigidbody2D>();
            ramen1rb.AddForce(-distanceRamen1 * 15);
            firstTime = false;
        }
        else
        {
            //Bomba1
            GameObject bomb1 = Instantiate(bomb, transform.position + new Vector3(-3, numRnd, -0.2f), Quaternion.identity);
            Vector3 distanceBomb1 = (transform.position - bomb1.transform.position);
            Rigidbody2D bomb1rb = bomb1.GetComponent<Rigidbody2D>();
            bomb1rb.AddForce(-distanceBomb1 * 15);
        }

        numRnd = rnd.Next(-2,0);
        //Bomba2
        GameObject bomb2 = Instantiate(bomb, transform.position + new Vector3(3, numRnd, -0.2f), Quaternion.identity);
        Vector3 distanceBomb2 = (transform.position - bomb2.transform.position);
        Rigidbody2D bomb2rb = bomb2.GetComponent<Rigidbody2D>();
        bomb2rb.AddForce(-distanceBomb2 * 15);

        numRnd = rnd.Next(-1, 2);
        //Bomba3
        GameObject bomb3 = Instantiate(bomb, transform.position + new Vector3(numRnd, -3, -0.2f), Quaternion.identity);
        Vector3 distanceBomb3 = (transform.position - bomb3.transform.position);
        Rigidbody2D bomb3rb = bomb3.GetComponent<Rigidbody2D>();
        bomb3rb.AddForce(-distanceBomb3 * 15);
    }



    //Metodo para restarle vida al BOSS
    public void takeDamage(int damage)
    {
        if (timeDamage <= 0f)
        {

            //Sonido
            mySpeaker.PlayOneShot(bossHit);

            //Activamos trigger "HIT", lanza animación de daño del jefe
            anim.SetTrigger("Hit");

            //Restamos la vida y reseteamos el tiempo entre daño
            sumo_health -= damage;
            timeDamage = 0.5f;


            //FASE 3
            if (sumo_health <= 0)
            {
                //Lanzamos la muerte del jefe y el fin de partida
                animInterfaz.SetTrigger("win");
                anim.SetBool("Dead", true);
                Invoke("LevelComplete", 6);
            }
            //FASE 2
            else if (sumo_health <= 30)
            {
                anim.SetInteger("Phase", 3);
            }
            //FASE 1
            else if (sumo_health <= 60)
            {
                anim.SetInteger("Phase", 2);
            }

        }
    }

    //Método para empujar a los juegadores si se acercan demasiado
    public void pushBackPlayers()
    {
        players.pushBack();
        players.takeDamage(3, 10);
    }

    //Fin de nivel
    public void LevelComplete()
    {
        players.RiseScore(3, 5000);
        PlayerPrefs.SetInt("ActualScore1", Player.score1);
        PlayerPrefs.SetInt("ActualScore2", Player.score2);
        SceneManager.LoadScene("Win");
    }

    //Sonido
    public void MakeSound (AudioClip clip)
    {
        mySpeaker.PlayOneShot(clip);
    }

    //Sonido pisotón
    public void MakeStampSound()
    {
        mySpeaker.PlayOneShot(stamp);
    }
}
