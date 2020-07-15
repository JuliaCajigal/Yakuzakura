using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiBehaviour : MonoBehaviour
{



    Animator animator;
    public Transform target;
    Quaternion iniRot;
    public bool patrolling;
    public bool chasing;
    public bool attacking;
    public bool stunned;
    public Transform targetDoor;
    public Transform targetDoor2;
    public Transform targetDoor3;
    public Transform targetDoor4;
    public AIDestinationSetter Astar;




    void Start()
    {
        Astar = GetComponentInParent<AIDestinationSetter>();
        animator = GetComponent<Animator>();
        iniRot = transform.rotation;
        //target = GameObject.FindGameObjectWithTag("Player1").transform;



    }


    void Update()
    {
        Astar.target = target;
        ManageAnimations();

    }


    void LateUpdate()
    {
       transform.rotation = iniRot;
    }






    private void ManageAnimations()
    {

        Vector3 direction = target.position - transform.position;
        direction.Normalize();


        //HACIA ARRIBA
        if ((direction.y >= 0.5) && (direction.x <= 0.5 && direction.x >= -0.5))
        {
            animator.SetBool("LEFT", false);
            animator.SetBool("UP", true);
            animator.SetBool("DOWN", false);
            animator.SetBool("RIGHT", false);
        }

        //HACIA ABAJO
        if ((direction.y < -0.5) && (direction.x <= 0.5 && direction.x >= -0.5))
        {
            animator.SetBool("LEFT", false);
            animator.SetBool("UP", false);
            animator.SetBool("DOWN", true);
            animator.SetBool("RIGHT", false);
        }

        //HACIA LA IZQUIERDA
        if ((direction.y < 0.5 && direction.y > -0.5) && (direction.x < 0))
        {
            animator.SetBool("LEFT", true);
            animator.SetBool("UP", false);
            animator.SetBool("DOWN", false);
            animator.SetBool("RIGHT", false);
        }

        //HACIA LA DERECHA
        if ((direction.y < 0.5 && direction.y > -0.5) && (direction.x > 0))
        {
            animator.SetBool("LEFT", false);
            animator.SetBool("UP", false);
            animator.SetBool("DOWN", false);
            animator.SetBool("RIGHT", true);
        }
    }


}

