using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {

           // myAudio.PlayOneShot(girlHit);
            Player.health2 -= 10f;
            Destroy(this.gameObject);
            Destroy(this.gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.tag == "Player1")
        {

            Destroy(this.gameObject);
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
