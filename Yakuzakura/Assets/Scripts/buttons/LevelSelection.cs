using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button Level1;
    public Button Level2;
    public Button Boss;

    // Start is called before the first frame update
    void Start()
    {
        Level1.onClick.AddListener(() => ChangeLevel("House"));
        Level2.onClick.AddListener(() => ChangeLevel("Garden"));
        Boss.onClick.AddListener(() => ChangeLevel("Boss"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeLevel(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }


}
