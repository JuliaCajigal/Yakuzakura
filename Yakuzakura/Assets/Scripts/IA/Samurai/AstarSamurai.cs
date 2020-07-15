using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AstarSamurai : AIPath
{


    SamuraiBehaviour samurai;
    System.Random rnd = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        samurai = GetComponentInChildren<SamuraiBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

   public override void OnTargetReached()
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

        //samurai.target = samurai.targetDoor2;
    }
}
