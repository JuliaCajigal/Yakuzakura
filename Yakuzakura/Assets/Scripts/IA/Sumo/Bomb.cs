using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float time;
    public float fuerza;
    public bool ready;
    public bool exploding;

    private Animator anim;
    private Rigidbody2D rb;
    private GameObject sumo;
    private SpriteRenderer spriteR;
    private GameObject player1;
    private Sumo_Health sumoHealth;
    public Sprite explodingSprite;


    void Start()
    {
        time = 5f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sumo = GameObject.FindGameObjectWithTag("Sumo");
        sumoHealth = sumo.GetComponent<Sumo_Health>();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        spriteR = GetComponentInChildren<SpriteRenderer>();
        
    }

 
    void Update()
    {
        time -= Time.deltaTime;

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
        //Cambia al sprite de explosion
        spriteR.sprite = explodingSprite;

        //Calculamos la distancia entre la bomba y el sumo, y con el jugador
        //Si está en rango les hará daño
        Vector3 directionSumo = this.transform.position - sumo.transform.position;
        var distanceSumo = directionSumo.magnitude;

        Vector3 directionPlayer = this.transform.position - player1.transform.position;
        var distancePlayer = directionPlayer.magnitude;


        if (distanceSumo <= 4)
        {

            sumoHealth.TakeDamage(20);
        }

        if(distancePlayer <=4)
        {
            Player.health1 -= 10;
        }
    
        Destroy(gameObject,0.3f);
    }


    //Maneja las colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if(collision.gameObject.tag == "Player2")
        {
            Player.score2 += 200;
            Vector3 direction = -(transform.position - sumo.transform.position).normalized;
            rb.AddForce(direction * fuerza);
        }
        if (collision.gameObject.tag == "Player1")
        {
            if (exploding == false)
            {
                Explode();
            }
        }

        if(collision.gameObject.tag == "Sumo")
        {
            Player.score2 += 200;
            Explode();
        }
    }

}
