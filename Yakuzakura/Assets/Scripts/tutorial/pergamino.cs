using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pergamino : MonoBehaviour
{

    private Rigidbody2D rb;
    private float speed = 2.3f;
    public bool clicked;
    //private Vector3 originalScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // originalScale = transform.localScale;
        MoveToDirection(Vector2.left);
        clicked = false;
    }



    private void FixedUpdate()
    {

        if (transform.position.x <= -8.5)
        {
            speed = 0;
            MoveToDirection(Vector2.right);
        }

        if (transform.position.x <= -8.5 && clicked == true)
        {
            speed = 2.3f;
            MoveToDirection(Vector2.right);
        }

        if (transform. position.x >= -4.5)
        {
            clicked = false;
            MoveToDirection(Vector2.left);
        }

    }

    private void MoveToDirection(Vector2 direction)
    {
        rb.velocity = direction * speed;

       // transform.localScale = new Vector3(direction.x < 0 ? -originalScale.x : originalScale.x, originalScale.y,
          //  originalScale.z);
    }
}