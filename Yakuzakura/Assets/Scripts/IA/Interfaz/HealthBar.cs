using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar1;
    public Image healthBar2;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public float maxHealth = 100f;
    public float health1;
    public float health2;

    // Start is called before the first frame update
    void Start()
    {
        //healthBar1 = GetComponent<Image>();
        health1 = Player.health1;
        health2 = Player.health2;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar1.fillAmount = Player.health1 / maxHealth;
        healthBar2.fillAmount = Player.health2 / maxHealth;
        score1.text =  Player.score1.ToString();
        score2.text = Player.score2.ToString();
    }
}
