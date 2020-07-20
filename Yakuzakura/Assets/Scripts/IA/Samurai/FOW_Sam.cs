using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FOW_Sam : MonoBehaviour
{

    //Controlar al enemigo
    //public SamuraiBehaviour samurai;


    public float viewRadius = 5;
    public float viewAngle;
    Collider2D[] playerInRadius;
    public LayerMask obstacleMask, playerMask,enemiesMask;
    public List<Transform> visiblePlayer = new List<Transform>();
    public SamuraiBehaviour samurai;

    private void Start()
    {

        samurai = GetComponentInChildren<SamuraiBehaviour>();
    }

    void FixedUpdate()
    {
        FindVisiblePlayer();
    }

    void FindVisiblePlayer()
    {
       playerInRadius = Physics2D.OverlapCircleAll(transform.position , viewRadius);

        visiblePlayer.Clear();

        for(int i = 0; i<playerInRadius.Length; i++)
        {
            Transform player = playerInRadius[i].transform;
            Vector2 dirPlayer = new Vector2((player.position.x - transform.position.x) + 1, (player.position.y - transform.position.y) + 1);
            dirPlayer = Rotate(dirPlayer, -90);

            if(Vector2.Angle(dirPlayer, transform.right) < viewAngle/2)
            {
                float distancePlayer = Vector2.Distance(transform.position, player.position);

                if(!Physics2D.Raycast(transform.position, dirPlayer, distancePlayer,enemiesMask) && !Physics2D.Raycast(transform.position, dirPlayer, distancePlayer, obstacleMask))
                {
                    visiblePlayer.Add(player);
                    if(playerInRadius[i].gameObject.tag=="Player1" || playerInRadius[i].gameObject.tag == "Player2")
                    {

                        samurai.patrolling = false;
                        samurai.chasing = true;
                    }

                }
            }
        }

    }


    public Vector2 DirFromAngle (float angleDeg, bool global)
    {
        if(!global)
        {
            angleDeg += transform.eulerAngles.z ;
        }
        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
    }

  
   public Vector2 Rotate(Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }
    
}
