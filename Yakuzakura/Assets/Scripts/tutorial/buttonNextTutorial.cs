using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonNextTutorial : MonoBehaviour
{

    public pergamino pergamino1;
    public pergamino2 pergamino2;
    public bool page2;
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
        if (page2 == false)
        {
            pergamino1.clicked = true;
            pergamino2.clicked = true;
            page2 = true;
        }else if (page2 == true)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
