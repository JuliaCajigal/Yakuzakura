                       using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Animator anim;
    public Animator animInterfaz;
    public GameObject wave;
    public GameObject shuriken;
    public GameObject bomb;
    public GameObject player;
    public Player players;
    public CameraShake camShake;
    System.Random rnd = new System.Random();
    int numRnd;

    //Vida y barra de vida
    public Image healthBar;
    public float maxHealth = 100f;
    public static float sumo_health;
    public float timeDamage;

    // Start is called before the first frame update
    void Start()
    {

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

        Debug.Log(anim.GetInteger("Phase"));
    }

    public void phaseOne()
    {
        //anim.SetTrigger("Throw");
        Debug.Log("--------------------------------ATAQUE FASE UNO");

        Instantiate(shuriken, transform.position + new Vector3(-3, 0, -0.2f), Quaternion.identity);
        Instantiate(shuriken, transform.position + new Vector3(3, 0, -0.2f), Quaternion.identity);
        StartCoroutine(camShake.Shake(.15f, .8f));


    }

    public void phaseTwo()
    {
        //anim.SetTrigger("Jump");
        Debug.Log("--------------------------------ATAQUE FASE DOS");

        Instantiate(wave, transform.position + new Vector3(0f, -1, -0.2f), Quaternion.identity);
        StartCoroutine(camShake.Shake(.15f, .8f));


    }

    public void phaseThree()
    {
        anim.SetTrigger("Throw");
        Debug.Log("--------------------------------ATAQUE FASE TRES");

        numRnd = rnd.Next(-2,0);
        //Bomba1
        GameObject bomb1 = Instantiate(bomb, transform.position + new Vector3(-3, numRnd, -0.2f), Quaternion.identity);
        Vector3 distanceBomb1 = (transform.position - bomb1.transform.position);
        Rigidbody2D bomb1rb = bomb1.GetComponent<Rigidbody2D>();
        bomb1rb.AddForce(-distanceBomb1 * 15);

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



    //Metodo para restarle vida
    public void takeDamage(int damage)
    {
        if (timeDamage <= 0f)
        {
            Debug.Log("-------------------------------------------------------------------" + anim.GetInteger("Phase"));
            Debug.Log(sumo_health);

            anim.SetTrigger("Hit");
            sumo_health -= damage;
            timeDamage = 0.5f;



            if (sumo_health <= 0)
            {
                animInterfaz.SetTrigger("win");
                anim.SetBool("Dead", true);
                Invoke("LevelComplete", 6);
            }
            else if (sumo_health <= 30)
            {
                anim.SetInteger("Phase", 3);
            }
            else if (sumo_health <= 60)
            {
                anim.SetInteger("Phase", 2);
            }

        }
    }

    public void pushBackPlayers()
    {
        players.pushBack();
        players.takeDamage(3, 10);
    }


    public void LevelComplete()
    {
        players.RiseScore(3, 5000);
        PlayerPrefs.SetInt("ActualScore1", Player.score1);
        PlayerPrefs.SetInt("ActualScore2", Player.score2);
        SceneManager.LoadScene("Win");
    }
}
