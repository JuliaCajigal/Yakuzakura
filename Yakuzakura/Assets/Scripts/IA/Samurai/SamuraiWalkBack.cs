using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiWalkBack : MonoBehaviour
{
    /*
    public SamuraiBehaviour samurai;
    //public Transform target;
    public float speed;
    float distance;
    */
    public SamuraiBehaviour samurai;
    public AstarSamurai samuraiAStar;
    public Vector3 moveVector;
    public Transform targetPlayer;
    public Rigidbody2D rb;
    public float speed = 100;

    float distance;
    

    // Start is called before the first frame update
    void Start()
    {
        speed = 100;
        samurai = GetComponentInChildren<SamuraiBehaviour>();
        samuraiAStar = GetComponent<AstarSamurai>();
        rb = GetComponent<Rigidbody2D>();

    }

// Update is called once per frame
void Update()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player2").transform;
        distance = (targetPlayer.position - transform.position).magnitude;
        moveVector = -(targetPlayer.position - transform.position).normalized;
        Debug.Log("---------------------DISTANCIA" + distance);


            Vector3 direction = (transform.position - targetPlayer.transform.position).normalized;

            rb.AddForce(direction * speed);
            Invoke("ReactivateAstar", 2);


    }

    public void ReactivateAstar()
    {
        //samuraiAStar.attacking = true;
        samurai.Astar.enabled = true;
        samurai.WalkBack.enabled = false;
    }


}
