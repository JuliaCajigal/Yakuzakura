using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadpixel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CambiarNivel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CambiarNivel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");

    }
}
