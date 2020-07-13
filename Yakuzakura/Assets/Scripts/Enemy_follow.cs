using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_follow : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip deathSnake;
    public AudioClip girlHit;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed;
    private bool playerNear = false;
    private GameObject player;
 
 

    //public GameObject FloatingText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player2");
        playerTransform = player.transform;
        gameObject.GetComponent<Animator>().enabled = false;

        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear == true)
        {
            followPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player2") || collision.CompareTag("Player1"))
        {
            myAudio.PlayOneShot(deathSnake);
            gameObject.GetComponent<Animator>().enabled = true;
            playerNear = true;
            Debug.Log("VOY A POR TI");
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
 
            myAudio.PlayOneShot(girlHit);
            Player.health2 -= 10f;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player1")
        {

            Destroy(this.gameObject);
        }
    }

    void showFloatingText()
    {

    }

    void followPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }


}
