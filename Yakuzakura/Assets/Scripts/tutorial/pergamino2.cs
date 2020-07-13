using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pergamino2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 2.8f;
    private float amplitude = 1f;
    public bool clicked;
    public page page;
    //private Vector3 originalScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // originalScale = transform.localScale;
        MoveToDirection(Vector2.right);
        clicked = false;
    }



    private void FixedUpdate()
    {
        Debug.Log(transform.position.x);
        if (transform.position.x >= 9.3)
        {
            speed = 0;
            MoveToDirection(Vector2.left);
        }

        if (transform.position.x >=9.3 && clicked == true)
        {
            speed = 2.8f;
            MoveToDirection(Vector2.left);
        }

        if (transform.position.x <= 4.3)
        {
            clicked = false;
            MoveToDirection(Vector2.right);
        }

        if(transform.position.x <= 4.3)
        {
            page.next = true;
        }

    }

    private void MoveToDirection(Vector2 direction)
    {
        rb.velocity = direction * speed;

        // transform.localScale = new Vector3(direction.x < 0 ? -originalScale.x : originalScale.x, originalScale.y,
        //  originalScale.z);
    }
}
