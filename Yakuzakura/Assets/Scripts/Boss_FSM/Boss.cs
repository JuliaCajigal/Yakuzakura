using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void phaseOne()
    {
        anim.SetTrigger("Throw");
        Debug.Log("--------------------------------ATAQUE FASE UNO");

    }

    public void phaseTwo()
    {
        anim.SetTrigger("Jump");
        Debug.Log("--------------------------------ATAQUE FASE DOS");
    }

    public void phaseThree()
    {
        anim.SetTrigger("Throw");
        Debug.Log("--------------------------------ATAQUE FASE TRES");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shuriken")
        {
            //takeDamage();
        }
    }
}
