using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip tapSound;
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


    // Start is called before the first frame update
    void Start()
    {

        score1 = PlayerPrefs.GetInt("ActualScore1");
        score2 = PlayerPrefs.GetInt("ActualScore2");
        //PlayerPrefs.SetInt("ActualScore2", 0);
        object1.orbiting = true;
        object2.orbiting = false;
        object2.zAxis = -object2.zAxis;
        health1 = 100f;
        health2 = 100f;
        gotKey = false;
        key.SetActive(false);
        numberOfPlayers = PlayerPrefs.GetInt("Players");

        if(numberOfPlayers == 1)
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

        if (Input.GetKeyDown(KeyCode.Escape)){
            PlayerPrefs.SetInt("ActualScore1", 0);
            PlayerPrefs.SetInt("ActualScore2", 0);
            SceneManager.LoadScene("Menu");

        }

        if (twoPlayers == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAudio.PlayOneShot(tapSound);
                object1.orbiting = !object1.orbiting;
                object2.orbiting = !object2.orbiting;

            }
        }

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


        if (Player.health1 <= 0 || Player.health2 <=0)
        {
            PlayerPrefs.SetInt("ActualScore1", score1);
            PlayerPrefs.SetInt("ActualScore2", score2);
            Death();
        }

        if(gotKey == true)
        {
            key.SetActive(true);
        }


    }

    void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void takeDamage(int pj, int damage)
    {
        Debug.Log("----------------------DAÑO");
        Debug.Log(timeDamage);

        if (timeDamage <= 0f)
        {
            timeDamage = 1f;

            if (pj == 1)
            {
                health1 -= damage;
                Debug.Log("----------------------DAÑO-----------------------");
            }
            else
            {
                health2 -= damage;
            }

            
        }
    }

    public void pushBack()
    {
        //metodo empujar
    }
}
