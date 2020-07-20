using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip tapSound;
    public AudioClip girlHit;
    public AudioClip boyHit;
    public Orbit object1;
    public Orbit2 object2;
    public static float health1;
    public static float health2;
    public float timeDamage;
    public static int score1;
    public static int score2 ;
    public static bool gotKey;
    public GameObject key;
    public bool twoPlayers;
    public int numberOfPlayers;
    Rigidbody2D rbplayer1;
    Rigidbody2D rbplayer2;
    Animator anim;



    void Start()
    {
        //Referencia parpadeo
        anim = GetComponent<Animator>();

        //Puntuación y vida
        score1 = PlayerPrefs.GetInt("ActualScore1");
        score2 = PlayerPrefs.GetInt("ActualScore2");
        health1 = 100f;
        health2 = 100f;

        //Variable controlar que pj gira
        object1.orbiting = true;
        object2.orbiting = false;
        object2.zAxis = -object2.zAxis;
        
        //Gestionar si tenemos la llave para el siguiente nivel
        gotKey = false;
        key.SetActive(false);
        
        //Controlar físicas de cada pj
        rbplayer1 = object1.GetComponent<Rigidbody2D>();
        rbplayer2 = object2.GetComponent<Rigidbody2D>();

        //Establecemos modo de juego
        numberOfPlayers = PlayerPrefs.GetInt("Players");
        if (numberOfPlayers == 1)
        {
            twoPlayers = false;
        }
        else if(numberOfPlayers == 2)
        {
            twoPlayers = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Tiempo entre ataques, recibir daño una vez
        timeDamage -= Time.deltaTime;

        
        //Botón ESC partida
        if (Input.GetKeyDown(KeyCode.Escape)){
            PlayerPrefs.SetInt("ActualScore1", 0);
            PlayerPrefs.SetInt("ActualScore2", 0);
            SceneManager.LoadScene("Menu");

        }

        //Controles un jugador
        if (twoPlayers == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAudio.PlayOneShot(tapSound);
                object1.orbiting = !object1.orbiting;
                object2.orbiting = !object2.orbiting;

            }
        }

        //Controles dos jugadores
        if (twoPlayers == true)
        {
            if (Input.GetKeyDown(KeyCode.A) && object1.orbiting == true)
            {
                myAudio.PlayOneShot(tapSound);
                object1.orbiting = !object1.orbiting;
                object2.orbiting = !object2.orbiting;
            }
            if (Input.GetKeyDown(KeyCode.L) && object2.orbiting == true)
            {
                myAudio.PlayOneShot(tapSound);
                object1.orbiting = !object1.orbiting;
                object2.orbiting = !object2.orbiting;
            }

        }

        //Muerte del jugador y puntuación final
        checkLife();

        //Activar llave en interfaz
        checkKey();


    }

    private void checkLife()
    {
        if (Player.health1 <= 0 || Player.health2 <= 0)
        {
            PlayerPrefs.SetInt("ActualScore1", score1);
            PlayerPrefs.SetInt("ActualScore2", score2);
            Death();
        }
    }

    private void checkKey()
    {
        
        if (gotKey == true)
        {
            key.SetActive(true);
        }
    }

    //Muerte
    void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    //Método para herir a los jugadores
    public void takeDamage(int pj, int damage)
    {

        if (timeDamage <= 0f)
        {
            timeDamage = 1f;

            //Daño pj1 (chico)
            if (pj == 1)
            {
                myAudio.PlayOneShot(boyHit);
                health1 -= damage;
                anim.SetTrigger("boyHit");

            }
            //Daño pj 2 (chica)
            else if(pj == 2)
            {
                myAudio.PlayOneShot(girlHit);
                anim.SetTrigger("girlHit");
                health2 -= damage;

            }
            //Dañar ambos jugadores
            else if (pj == 3)
            {
                myAudio.PlayOneShot(boyHit);
                myAudio.PlayOneShot(girlHit);
                anim.SetTrigger("bothHit");
                health1 -= damage;
                health2 -= damage;

            }
        }
    }


    //Metodo para aumentar las puntuaciones del jugador
    public void RiseScore(int pj, int points)
    {
            if (pj == 1)
            {
                score1 += points;

            }
            else if (pj == 2)
            {
                score2 += points;

            }
            else if (pj == 3)
            {
                score1 += points;
                score2 += points;
            }

    }
    
    //Método para empujar a los jugadores
    public void pushBack()
    {

        rbplayer1.AddForce(Vector2.down * 3000);
        rbplayer2.AddForce(Vector2.down * 3000);

    }


}
