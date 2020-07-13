using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI textscore1;
    public TextMeshProUGUI textscore2;
    public TextMeshProUGUI textscoreTotal;
    int score1;
    int score2;

    // Start is called before the first frame update
    void Start()
    {
        score1 = PlayerPrefs.GetInt("ActualScore1");
        score2 = PlayerPrefs.GetInt("ActualScore2");
        textscore1.text = score1.ToString();
        textscore2.text = score2.ToString();
        PlayerPrefs.SetInt("ActualScore1", 0);
        PlayerPrefs.SetInt("ActualScore2", 0);
        int total = score1 + score2;
        textscoreTotal.text = "TOTAL SCORE\n" + total.ToString();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
