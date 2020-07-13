using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCredits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {

        PlayerPrefs.SetInt("ActualScore1", 0);
        PlayerPrefs.SetInt("ActualScore2", 0);
        // source.PlayOneShot(sonido);
        SceneManager.LoadScene("Credits");
    }
}
