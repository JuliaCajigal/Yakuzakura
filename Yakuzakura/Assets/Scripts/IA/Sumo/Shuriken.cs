using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Shuriken : MonoBehaviour
{

    GameObject sumo;
    AIDestinationSetter Astar;
    Rigidbody2D rb;
    GameObject player;
    Sumo_Health sumoHealth;
    public bool againstPlayer;

    

    void Start()
    {
        againstPlayer = true;
        sumo = GameObject.FindGameObjectWithTag("Sumo");
        sumoHealth = sumo.GetComponent<Sumo_Health>();
        Astar = GetComponent<AIDestinationSetter>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player1");
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
            againstPlayer = false;
            Astar.target = sumo.transform;

        }
        //Si golpea al chico le resta puntos de vida y se destruye 
        if(collision.gameObject.tag=="Player1")
        {
            if (againstPlayer == true)
            {
                Player.health1 -= 10;
                Destroy(gameObject);
            }
        }
        //Si golpea al sumo, despues de haber sido redireccionado por la chica, le resta vida
        if (collision.gameObject.tag == "Sumo")
        {
            if (againstPlayer == false)
            {
                sumoHealth.TakeDamage(20);
                Destroy(gameObject);
            }
        }
    }



}
