using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOW : MonoBehaviour
{

    //Controlar al enemigo
    public ShooterBehaviour shooter;


    public float viewRadius = 5;
    public float viewAngle;
    Collider2D[] playerInRadius;
    public LayerMask obstacleMask, playerMask,enemiesMask;
    public List<Transform> visiblePlayer = new List<Transform>();

    private void Start()
    {
        shooter = GetComponent<ShooterBehaviour>();
    }

    void FixedUpdate()
    {
        FindVisiblePlayer();
    }

    void FindVisiblePlayer()
    {
       playerInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);

        visiblePlayer.Clear();

        for(int i = 0; i<playerInRadius.Length; i++)
        {
            Transform player = playerInRadius[i].transform;
            Vector2 dirPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

            if(Vector2.Angle(dirPlayer, transform.right) < viewAngle/2)
            {
                float distancePlayer = Vector2.Distance(transform.position, player.position);

                if(!Physics2D.Raycast(transform.position, dirPlayer, distancePlayer,enemiesMask))
                {
                    visiblePlayer.Add(player);
                    if(playerInRadius[i].gameObject.tag=="Player1" || playerInRadius[i].gameObject.tag == "Player2")
                    {
                        shooter.patrolling = false;
                        shooter.shooting = true;
                    }

                }
            }
        }

    }


    public Vector2 DirFromAngle (float angleDeg, bool global)
    {
        if(!global)
        {
            angleDeg += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
    }
}
