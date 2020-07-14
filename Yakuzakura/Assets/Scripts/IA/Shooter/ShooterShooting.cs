using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterShooting : MonoBehaviour
{


    private Transform playerTransform;
    private GameObject player;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 movement;

    //Variables de disparo
    public GameObject bullet;
    public float FireRate;
    public float nextFire;

    //Sonidos
    public AudioSource myAudio;
    public AudioClip shoot;


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player1");
        playerTransform = player.transform;
        rb = this.GetComponent<Rigidbody2D>();
        nextFire = Time.time;

    }


    void Update()
    {

            RotateTo();
            CheckIfTimeToFire();
       
    }


    void RotateTo()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            myAudio.PlayOneShot(shoot);
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + FireRate;
        }
    }


}
