using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enimyShooter : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip shoot;



    private Transform playerTransform;
    private GameObject player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool enemyNear;


    public GameObject bullet;
    public float FireRate;
    public float nextFire;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1");
        playerTransform = player.transform;

        enemyNear = false;
        rb = this.GetComponent<Rigidbody2D>();


        nextFire = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyNear)
        {
            RotateTo();
            CheckIfTimeToFire();
        }


    }

    void RotateTo()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player1"))
        {


            enemyNear = true;

        }

    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            myAudio.PlayOneShot(shoot);
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + FireRate;
        }
    }
}
