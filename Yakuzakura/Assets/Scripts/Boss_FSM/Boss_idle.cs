﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_idle : StateMachineBehaviour
{
    public float pushRange;
    float attackDelay;

    GameObject player1;
    GameObject player2;
    Rigidbody2D rb;
    Boss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();

        //Debug.Log(player1.position);
        //Debug.Log(rb.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distance1 = (player1.transform.position - rb.transform.position).magnitude;
        var distance2 = (player2.transform.position - rb.transform.position).magnitude;
        //Debug.Log(distance1);

        if (distance1 <= pushRange || distance2 <= pushRange)
        {
            
            animator.SetTrigger("Push");
        }

        attackDelay -= Time.deltaTime;

        if(attackDelay <= 0f)
        {
            animator.SetTrigger("Throw");
            boss.phaseOne();
            attackDelay = 5f;
        }
        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Push");
        animator.ResetTrigger("Throw");
    }

}
