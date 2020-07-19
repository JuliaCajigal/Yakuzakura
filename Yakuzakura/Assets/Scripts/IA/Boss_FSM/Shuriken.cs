﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Shuriken : MonoBehaviour
{
    public Animator anim;
    GameObject sumo;
    AIDestinationSetter Astar;
    Rigidbody2D rb;
    GameObject player;
    GameObject players;
    Player playersData;
    private Boss boss;
    public bool againstPlayer;

    

    void Start()
    {
        againstPlayer = true;
        sumo = GameObject.FindGameObjectWithTag("Sumo");
        boss = sumo.GetComponent<Boss>();
        anim = boss.anim;
        Astar = GetComponent<AIDestinationSetter>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player1");
        players = GameObject.FindGameObjectWithTag("Players");
        playersData = players.GetComponent<Player>();
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
