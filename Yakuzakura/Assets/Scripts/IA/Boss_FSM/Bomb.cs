using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //Físicas bomba
    public float time;
    public float fuerza;
    public bool ready;
    public bool exploding;
    public bool touchedPlayer;
    private Rigidbody2D rb;

    //Referencias a pjs
    private GameObject player1;
    private GameObject player2;
    private GameObject players;
    private Player player;

    //Referencia a boss
    private Boss boss;
    private GameObject sumo;

    //Animaciones
    public Sprite explodingSprite;
    private SpriteRenderer spriteR;
    private Animator anim;
    

    //Temblor camara
    public CameraShake camShake;

    //Audio
    private SoundsManager soundManager;


    void Start()
    {
        //Tiempo que tardan en explotar
        time = 9f;

        //Sonido
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<SoundsManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sumo = GameObject.FindGameObjectWithTag("Sumo");
        boss = sumo.GetComponent<Boss>();

        //Referencias a jugadores
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        players = GameObject.FindGameObjectWithTag("Players");
        player = players.GetComponent<Player>();
        spriteR = GetComponentInChildren<SpriteRenderer>();

        //Temblor
        camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

    }

 
    void Update()
    {
        time -= Time.deltaTime;

        //Tiempo de vida de la bomba y sus animaciones
        if (time <= 3)
        {
            readyToExplode();
        }

        if(time <= 0 && exploding == false)
        {
            exploding = true;
            Explode();
        }

    }


    //Metodo que inicia la animacion de parpadear cuando está a punto de explotar
    private void readyToExplode()
    {
        anim.SetBool("red", true);
    }


    //Metodo para que explote la bomba
    private void Explode()
    {

        soundManager.makeSound(soundManager.explosion);
        //Cambia al sprite de explosion
        //spriteR.sprite = explodingSprite;
        anim.SetTrigger("explode");

        //Calculamos la distancia entre la bomba y el sumo, y con el jugador
        //Si está en rango les hará daño
        Vector3 directionSumo = this.transform.position - sumo.transform.position;
        var distanceSumo = directionSumo.magnitude;

        Vector3 directionPlayer = this.transform.position - player1.transform.position;
        var distancePlayer = directionPlayer.magnitude;

        Vector3 directionPlayer2 = this.transform.position - player2.transform.position;
        var distancePlayer2 = directionPlayer2.magnitude;

        //Gestión daño bomba
        if (distanceSumo <= 4 && touchedPlayer == false)
        {

            boss.takeDamage(10);
        }

        if(distancePlayer<=4 && distancePlayer2 <= 4)
        {
            player.takeDamage(3, 10);
        }

        if(distancePlayer <=4)
        {
            player.takeDamage(1, 10);
        }

        if(distancePlayer2 <=4)
        {
            player.takeDamage(2, 10);
        }

        //Temblor cámara
        StartCoroutine(camShake.Shake(.15f, .8f));

        //Destrucción
        Destroy(gameObject,0.5f);
    }


    //Maneja las colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if(collision.gameObject.tag == "Player2")
        {
            player.RiseScore(2, 243);
            Vector3 direction = -(transform.position - sumo.transform.position).normalized;
            rb.AddForce(direction * fuerza);
        }
        if (collision.gameObject.tag == "Player1")
        {
            if (exploding == false)
            {
                touchedPlayer = true;
                Explode();
            }
        }
    }
}
