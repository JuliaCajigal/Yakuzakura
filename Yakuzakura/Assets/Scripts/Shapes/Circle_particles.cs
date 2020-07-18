using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_particles : MonoBehaviour
{
    public ParticleSystem ps;
    private float radius;
    private bool bigger;
    Player player;
    Boss boss;


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        radius = 0.05f;
        bigger = true;
        player = GameObject.FindGameObjectWithTag("Players").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Sumo").GetComponent<Boss>();

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

        if (other.tag == "Player2")
        {
            Debug.Log(other.tag);
            bigger = false;

        }
        else if (other.tag == "Player1" && bigger)
        {
            Debug.Log("-----------------------------------------PLYR2"+bigger);

            player.takeDamage(1, 10);
            Destroy(gameObject);
        }

        if (other.tag == "Sumo" && !bigger)
        {
            
            boss.takeDamage(50);
            Destroy(gameObject, 0.4f);
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
    }

}
