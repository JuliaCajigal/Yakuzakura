using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AstarSamurai : AIPath
{


    public SamuraiBehaviour samurai;
    System.Random rnd = new System.Random();
    public bool attacking;
    GameObject player;
    Player playerData;



    void Start()
    {
        attacking = true;
        samurai = GetComponentInChildren<SamuraiBehaviour>();
        player = GameObject.FindGameObjectWithTag("Players");
        playerData = player.GetComponent<Player>();

    }


    void Update()
    {
        
    }
    

   public override void OnTargetReached()
    {

        //PATRULLAR - Cambia de objetivo aleatorio entre las puertas
        //de la sala, cuando ya ha alcanzado una
        if (samurai.patrolling == true)
        {
            int n = rnd.Next(0, 4);

            Debug.Log("NUMERO RANDOM: " + n);
            switch (n)
            {
                case 0:
                    samurai.target = samurai.targetDoor;
                    break;
                case 1:
                    samurai.target = samurai.targetDoor2;
                    break;
                case 2:
                    samurai.target = samurai.targetDoor3;
                    break;
                case 3:
                    samurai.target = samurai.targetDoor4;
                    break;
            }

        }

        //PERSEGUIR - Si el Samurai está en modo perseguir y llega a su
        //objetivo, realiza la animación de atacar y se activa el modo
        //RETROCEDER
        if (samurai.chasing == true)
        {

                samurai.animator.SetTrigger("ONEATTACK");
                samurai.Astar.enabled = false;
                //Se activa el Script que tiene el modo RETROCEDER
                samurai.WalkBack.enabled = true;
                playerData.takeDamage(2, 5);


        }
 


    }

    public void stopAttacking()
    {

        samurai.animator.SetBool("ATTACK", false);
    }
}
