using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip musica1;

    // Start is called before the first frame update
    void Start()
    {
        myAudio.PlayOneShot(musica1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        PlayerPrefs.SetInt("ActualScore1", 0);
        PlayerPrefs.SetInt("ActualScore2", 0);
        Application.Quit();
    }
}
