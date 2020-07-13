using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SnakeBehaviour : MonoBehaviour
{

    AIPath pathfinder;
    public Animator animator;
    GameObject player;
    AIDestinationSetter destinationSetter;
    Orbit2 chica;
    Orbit chico;
    bool sleeping = true;

    // Start is called before the first frame update
    void Start()
    {

        pathfinder = GetComponent<AIPath>();
        pathfinder.enabled=false;
        player = GameObject.FindGameObjectWithTag("Player2");
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = player.transform;
        chica = (Orbit2)FindObjectOfType(typeof(Orbit2));
        chico = (Orbit)FindObjectOfType(typeof(Orbit));

    }

    // Update is called once per frame
    void Update()
    {

    }


    //Estado en el que la serpiente está dormida
    //Si alguno de los protagonistas se encuentra dentro de su area de escucha y rompe algún objeto
    //pasa al estado de perseguir al jugador.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (sleeping == true)
        {
            if (collision.CompareTag("Player2") || collision.CompareTag("Player1"))
            {

                if (chico.makeSound == true || chica.makeSound == true)
                {
                    ChasingState();
                    sleeping = false;

                }
            }
        }
    }


    //Estado en el que la serpiente persigue al jugador
    //Se activa el componente que realiza pathfinding mediante A*
    private void ChasingState()
    {
        if (pathfinder.enabled == false)
        {
            AstarPath.active.Scan();
            animator.SetBool("chasing", true);
            pathfinder.enabled = true;
        }
    }
    
}
