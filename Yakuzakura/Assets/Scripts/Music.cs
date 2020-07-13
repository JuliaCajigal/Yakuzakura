using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip musica2;

    // Start is called before the first frame update
    void Start()
    {

        myAudio.PlayOneShot(musica2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
