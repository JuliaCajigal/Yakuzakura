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
        //Referencia a enemigo
        samurai = GetComponentInChildren<SamuraiBehaviour>();
    }

    void FixedUpdate()
    {
        //Encontrar jugadores visibles
        FindVisiblePlayer();
    }

    void FindVisiblePlayer()
    {
        //Busca los jugadores que se encuentran en un radio dado
        playerInRadius = Physics2D.OverlapCircleAll(transform.position , viewRadius);

        visiblePlayer.Clear();

        //Vamos iterando sobre los pj encontrados en dicho radio
        for (int i = 0; i<playerInRadius.Length; i++)
        {
            //Comprobamos la dirección del enemigo con el jugador
            Transform player = playerInRadius[i].transform;
            Vector2 dirPlayer = new Vector2((player.position.x - transform.position.x) + 1, (player.position.y - transform.position.y) + 1);

            //Rotamos al jugador, ya que aparecía desfasado
            dirPlayer = Rotate(dirPlayer, -90);

            //Si es menor que el radio de visión entonces es sensible de ser detectado
            if (Vector2.Angle(dirPlayer, transform.right) < viewAngle/2)
            {
                //Calculamos que la distancia con el jugador
                float distancePlayer = Vector2.Distance(transform.position, player.position);

                //Y finalmente comprobamos si se superpone con la máscara de enemigos y de obstáculos, para permitirnos ocultarnos trás un objeto o una pared
                if (!Physics2D.Raycast(transform.position, dirPlayer, distancePlayer,enemiesMask) && !Physics2D.Raycast(transform.position, dirPlayer, distancePlayer, obstacleMask))
                {
                    visiblePlayer.Add(player);

                    //Si coincide el tag con el de los jugadores, el enemigo pasará a modo perseguir
                    if (playerInRadius[i].gameObject.tag=="Player1" || playerInRadius[i].gameObject.tag == "Player2")
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

  
    //Método para rotar cono de visión 90º
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
