using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public AudioSource myAudio;
    public AudioClip repelBullet;
    public AudioClip manHit;

    public float moveSpeed;
    Rigidbody2D rb;

    GameObject target;
    Player playerData;
    Vector2 moveDirection;
    public bool explode;


    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponent<Animator>().enabled = false;

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player1");
        playerData = target.GetComponentInParent<Player>();
        move();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (explode == false)
        {
            if (collision.gameObject.tag != "EnemyShooter")
            {
                Debug.Log(collision.gameObject.tag);
                explode = true;
                moveSpeed = 0;
                move();
                gameObject.GetComponent<Animator>().enabled = true;
                Destroy(gameObject, 0.5f);
                if (collision.gameObject.tag == "Player1")
                {
                    myAudio.PlayOneShot(manHit);
                    playerData.takeDamage(1, 10);
                }

                if (collision.gameObject.tag == "Player2")
                {
                    myAudio.PlayOneShot(repelBullet);
                    playerData.RiseScore(2, 125);
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void move()
    {
        //Movimiento hacia el personaje
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);


        //Rotacion hacia el personaje
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
