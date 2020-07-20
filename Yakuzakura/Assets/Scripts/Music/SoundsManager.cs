using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    AudioSource mySpeaker;
    public AudioClip explosion;

    // Start is called before the first frame update
    void Start()
    {
        mySpeaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void makeSound(AudioClip clip)
    {
        mySpeaker.PlayOneShot(clip);
    }


}
