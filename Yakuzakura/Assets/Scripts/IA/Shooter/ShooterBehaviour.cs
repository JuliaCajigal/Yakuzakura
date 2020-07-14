using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehaviour : MonoBehaviour
{

    Rigidbody2D m_Rigidbody;
    ShooterPatrol patrol;
    ShooterShooting shoot;
    GameObject player;

    //Controladores de comportamientos
    public bool patrolling;
    public bool shooting;


    void Start()
    {

        patrolling = true;
        shooting = false;

        player = GameObject.FindWithTag("Player1");
        m_Rigidbody = GetComponent<Rigidbody2D>();
        patrol = GetComponent<ShooterPatrol>();
        shoot = GetComponent<ShooterShooting>();
        shoot.enabled = false;

    }

    void Update()
    {

        if (patrolling == true)
        {
            patrol.enabled = true;
 
        }
        else
        {
            patrol.enabled = false;

        }

        if (shooting == true)
        {
            shoot.enabled = true;

            Vector3 direction = this.transform.position - player.transform.position;
            var distance = direction.magnitude;
            Debug.Log(distance);
            if(distance >= 10)
            {
                shooting = false;
                patrolling = true;
            }
        }
        else
        {
            shoot.enabled = false;
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (patrolling == true)
        {


            if (collision.gameObject.tag == "Player2")
            {
                patrolling = false;
                shooting = true;

            }

        }
    }
}
