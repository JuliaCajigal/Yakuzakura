using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page : MonoBehaviour
{
    public Sprite page2;
    public bool next;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (next == true)
        {
            this.GetComponent<SpriteRenderer>().sprite = page2;
        }
    }


}
