                       using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator anim;
    public GameObject wave;
    public GameObject shuriken;
    public GameObject bomb;

    //Vida y barra de vida
    public Image healthBar;
    public float maxHealth = 100f;
    public static float sumo_health;
    public float timeDamage;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        sumo_health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //Tiempo entre ataques, recibir daño una vez
        timeDamage -= Time.deltaTime;

        //Barra de vida
        healthBar.fillAmount = sumo_health / maxHealth;

        Debug.Log(anim.GetInteger("Phase"));
    }

    public void phaseOne()
    {
        //anim.SetTrigger("Throw");
        Debug.Log("--------------------------------ATAQUE FASE UNO");

        Instantiate(shuriken, transform.position + new Vector3(-3, 0, -0.2f), Quaternion.identity);
        Instantiate(shuriken, transform.position + new Vector3(3, 0, -0.2f), Quaternion.identity);


    }

    public void phaseTwo()
    {
        //anim.SetTrigger("Jump");
        Debug.Log("--------------------------------ATAQUE FASE DOS");

        Instantiate(wave, transform.position + new Vector3(-0.5f, 0, -0.2f), Quaternion.identity);


    }

    public void phaseThree()
    {
        anim.SetTrigger("Throw");
        Debug.Log("--------------------------------ATAQUE FASE TRES");

        Instantiate(bomb, transform.position + new Vector3(-3, 0, -0.2f), Quaternion.identity);
        Instantiate(bomb, transform.position + new Vector3(3, 0, -0.2f), Quaternion.identity);
        Instantiate(bomb, transform.position + new Vector3(-3, 0, -0.2f), Quaternion.identity);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shuriken")
        {
            //takeDamage();
        }
    }

    //Metodo para restarle vida
    public void takeDamage(int damage)
    {
        if (timeDamage <= 0f)
        {
            Debug.Log("-------------------------------------------------------------------" + anim.GetInteger("Phase"));
            Debug.Log(sumo_health);

            anim.SetTrigger("Hit");
            sumo_health -= damage;
            timeDamage = 0.5f;



            if (sumo_health <= 0)
            {
                anim.SetBool("Dead", true);
            }
            else if (sumo_health <= 30)
            {
                anim.SetInteger("Phase", 3);
            }
            else if (sumo_health <= 60)
            {
                anim.SetInteger("Phase", 2);
            }

        }
    }
}
