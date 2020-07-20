using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_idle : StateMachineBehaviour
{
    //Distancia mínima jugadores y fase
    private float pushRange = 3f;
    int phase;

    //Tiempo entre ataque de la primera fase
    float attackDelay = 2f;

    //Players
    GameObject player1;
    GameObject player2;

    //Animator
    Rigidbody2D rb;
    Boss boss;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Players
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        
        //Animator
        boss = animator.GetComponent<Boss>();
        rb = animator.GetComponent<Rigidbody2D>();

        //Referenciamos la fase en la que nos encontramos
        phase = animator.GetInteger("Phase");
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Comprobamos distancia entre pj-boss
        checkDistance(animator);

        //Chekeamos la fase para lanzar el próximo ataque
        checkPhase(animator);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Reseteamos los triggers para que no se lancen más de una vez como prevención
        animator.ResetTrigger("Push");
        animator.ResetTrigger("Throw");
        animator.ResetTrigger("ThrowRed");
        animator.ResetTrigger("Jump");
    }

    private void checkDistance(Animator anim)
    {
        var distance1 = (player1.transform.position - rb.transform.position).magnitude;
        var distance2 = (player2.transform.position - rb.transform.position).magnitude;

        //Si estamos a menos distancia lanzamos el trigger push y empunamos a los jugadores
        if (distance1 <= pushRange || distance2 <= pushRange)
        {
            anim.SetTrigger("Push");
        }
    }

    private void checkPhase(Animator anim)
    {
        attackDelay -= Time.deltaTime;
        
        //Dependiendo de la fase en la que nos encontremos lanzaremos un ataque u otro
        //con una frecuencia de tiempo distinta
        if (attackDelay <= 0f)
        {
            if (phase == 1)
            {
                anim.SetTrigger("Throw");
                attackDelay = 4f;

            }
            else if (phase == 2)
            {
                anim.SetTrigger("Jump");
                attackDelay = 4f;
            }
            else if (phase == 3)
            {
                anim.SetTrigger("ThrowRed");
                attackDelay = 10f;
            }
        }
    }

}
