using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_particles : MonoBehaviour
{
    //Referencias boss y pjs
    public Animator anim;
    Boss boss;
    Player player;

    //Físicas efecto partículas
    public ParticleSystem ps;
    private float radius;
    private bool bigger;

    //Sonido
    AudioSource mySpeaker;
    public AudioClip clink;
   
  
    void Start()
    {
        //Físicas
        ps = GetComponent<ParticleSystem>();
        radius = 0.05f;
        bigger = true;
        
        //Boss
        boss = GameObject.FindGameObjectWithTag("Sumo").GetComponent<Boss>();
        anim = boss.anim;
        player = GameObject.FindGameObjectWithTag("Players").GetComponent<Player>();

        //Audio
        mySpeaker = GetComponent<AudioSource>();
    }

 
    void Update()
    {
        //Gestiona si la onda expansiva está creciendo
        if (bigger)
        {
            Bigger();
        }
        //O decreciendo
        else
        {
            Smaller();
        }
    }

    //Colisiones
    void OnParticleCollision(GameObject other)
    {
        //Colisión jugador 2 (chica): rebota la onda
        if (other.tag == "Player2" && bigger)
        {
            mySpeaker.PlayOneShot(clink);
            Player.score2 += 200;        
            bigger = false;

        }
        //Colisión jugador 1 (chico): solo recibe daño si la onda está creciendo
        else if (other.tag == "Player1" && bigger)
        {

            player.takeDamage(1, 10);
            Destroy(gameObject);
        }

        //Colisión Boss: si está decreciendo recibe daño
        if (other.tag == "Sumo" && !bigger)
        {
            if (anim.GetInteger("Phase") == 2)
            {
                boss.takeDamage(50);
            }
            
            Destroy(gameObject, 0.4f);
        }
    }

    //Crecimiento onda
    void Bigger()
    {
        var sh = ps.shape;
        sh.radius += radius;
    }

    //Decrecimiento onda
    void Smaller()
    {
        var sh = ps.shape;
        sh.radius -= radius;
    }

}
