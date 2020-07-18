using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sumo_Health : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 500f;
    public static float sumo_health;

    // Start is called before the first frame update
    void Start()
    {
        sumo_health = 500f;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = sumo_health / maxHealth;

    }

    //Metodo para restarle vida
    public void TakeDamage(int damage)
    {
        sumo_health -= damage;
    }

}
