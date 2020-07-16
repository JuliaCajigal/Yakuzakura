using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiBehaviour : MonoBehaviour
{



    public Animator animator;
    public Transform target;
    Quaternion iniRot;
    public bool patrolling;
    public bool chasing;
    public bool attacking;
    public Transform targetDoor;
    public Transform targetDoor2;
    public Transform targetDoor3;
    public Transform targetDoor4;
    public Transform targetPlayer;

    public AIDestinationSetter Astar;
    public SamuraiWalkBack WalkBack;




    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player2").transform;
        patrolling = true;
        Astar = GetComponentInParent<AIDestinationSetter>();
        WalkBack = GetComponentInParent<SamuraiWalkBack>();
        WalkBack.enabled = false;
        animator = GetComponent<Animator>();
        iniRot = transform.rotation;




    }


    void Update()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player2").transform;

        Astar.target = target;
        ManageAnimations();
       
        if(patrolling == false && chasing == true)
        {
            animator.SetBool("CHASE", true);
            target = targetPlayer;
            
        }
        

    }


    void LateUpdate()
    {
       transform.rotation = iniRot;
    }



    private void ManageAnimations()
    {

        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        if (patrolling == true)
        {
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

        if (chasing == true)
        {
            //HACIA ARRIBA
            if ((direction.y >= 0.5) && (direction.x <= 0.5 && direction.x >= -0.5))
            {
                animator.SetBool("LEFT_CHASE", false);
                animator.SetBool("UP_CHASE", true);
                animator.SetBool("DOWN_CHASE", false);
                animator.SetBool("RIGHT_CHASE", false);
            }

            //HACIA ABAJO
            if ((direction.y < -0.5) && (direction.x <= 0.5 && direction.x >= -0.5))
            {
                animator.SetBool("LEFT_CHASE", false);
                animator.SetBool("UP_CHASE", false);
                animator.SetBool("DOWN_CHASE", true);
                animator.SetBool("RIGHT_CHASE", false);
            }

            //HACIA LA IZQUIERDA
            if ((direction.y < 0.5 && direction.y > -0.5) && (direction.x < 0))
            {
                animator.SetBool("LEFT_CHASE", true);
                animator.SetBool("UP_CHASE", false);
                animator.SetBool("DOWN_CHASE", false);
                animator.SetBool("RIGHT_CHASE", false);
            }

            //HACIA LA DERECHA
            if ((direction.y < 0.5 && direction.y > -0.5) && (direction.x > 0))
            {
                animator.SetBool("LEFT_CHASE", false);
                animator.SetBool("UP_CHASE", false);
                animator.SetBool("DOWN_CHASE", false);
                animator.SetBool("RIGHT_CHASE", true);
            }
        }
    }


}

