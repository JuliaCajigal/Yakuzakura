using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Shuriken : MonoBehaviour
{
    //Referencias a pjs
    private GameObject player2;
    private GameObject player;
    private GameObject players;
    private Player playersData;
    public bool againstPlayer;

    //Físicas
    private AIDestinationSetter Astar;
    private Rigidbody2D rb;

    //Animaciones
    public Animator anim;

    //Referencias a boss
    private Boss boss;
    private GameObject sumo;

    //Audio
    AudioSource mySpeaker;
    public AudioClip clink;


    void Start()
    {
        //Boss
        sumo = GameObject.FindGameObjectWithTag("Sumo");
        boss = sumo.GetComponent<Boss>();

        //Animaciones y audio
        mySpeaker = GetComponent<AudioSource>();
        anim = boss.anim;
        rb = GetComponent<Rigidbody2D>();

        //Referencia pjs
        player = GameObject.FindGameObjectWithTag("Player1");
        players = GameObject.FindGameObjectWithTag("Players");
        playersData = players.GetComponent<Player>();
        againstPlayer = true;

        //Físicas
        Astar = GetComponent<AIDestinationSetter>();
        Astar.target = player.transform;
    }

    void Update()
    {

    }


    //Controlador de las colision
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Cuando es golpeado por la chica, el shuriken cambia su objetivo hacia el Boss
        //Ademas deja de poder hacerle daño al Chico
        if (collision.gameObject.tag == "Player2")
        {
            mySpeaker.PlayOneShot(clink);
            playersData.RiseScore(2, 217);
            againstPlayer = false;
            Astar.target = sumo.transform;

        }
        //Si golpea al chico le resta puntos de vida y se destruye 
        if(collision.gameObject.tag=="Player1")
        {
            if (againstPlayer == true)
            {
                playersData.takeDamage(1, 10);
                Destroy(gameObject);
            }
        }
        //Si golpea al sumo, despues de haber sido redireccionado por la chica, le resta vida
        if (collision.gameObject.tag == "Sumo")
        {
            if (againstPlayer == false)
            {
                if (anim.GetInteger("Phase") == 1)
                {
                    boss.takeDamage(20);
                }
                Destroy(gameObject);
            }
        }
    }
}
