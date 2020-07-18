using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_particles : MonoBehaviour
{
    public ParticleSystem ps;
    private float radius;
    private bool bigger;
    float attackDelay;
    Player player;
    Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        radius = 0.05f;
        bigger = true;
        attackDelay = 5f;
        player = GameObject.FindGameObjectWithTag("Players").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Players").GetComponent<Boss>();

    }

    // Update is called once per frame
    void Update()
    {
       

        if (bigger)
        {
            Bigger();
        }
        else
        {
            Smaller();
        }
        
        
    }

    void OnParticleCollision(GameObject other)
    {

        if (other.tag == "Player1")
        {
            Debug.Log(other.tag);
            bigger = false;
            player.takeDamage(1,10);
            Destroy(gameObject);

        }
        else if (other.tag == "Player2")
        {
            Debug.Log(other.tag);
            bigger = false;
        }

        if (other.tag == "Sumo" && !bigger)
        {
            Destroy(gameObject);
            boss.takeDamage(50);
        }
    }

    void Bigger()
    {
        var sh = ps.shape;
        sh.radius += radius;
    }

    void Smaller()
    {
        var sh = ps.shape;
        sh.radius -= radius;

        if (sh.radius <= 2)
        {
            //bigger = true;
        }
    }
}
