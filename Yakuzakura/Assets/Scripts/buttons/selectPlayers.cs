using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectPlayers : MonoBehaviour
{
    public int Players;

    public Sprite TwoPlayers;
    public Sprite OnePlayer;

    // Start is called before the first frame update
    void Start()
    {
        Players = 1;
        PlayerPrefs.SetInt("Players", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (Players == 1)
        {
            
            PlayerPrefs.SetInt("Players", 2);
            this.GetComponent<Image>().sprite = TwoPlayers;
            Players = 2;
        }
        
        
       else if (Players == 2) { 
            
            PlayerPrefs.SetInt("Players", 1);
            this.GetComponent<Image>().sprite = OnePlayer;
            Players = 1;
        }
        
        
        Debug.Log(PlayerPrefs.GetInt("Players"));
    }
}
