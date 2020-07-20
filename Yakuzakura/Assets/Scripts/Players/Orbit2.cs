using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class Orbit2 : MonoBehaviour
{
    //Sonidos
    public AudioSource myAudio;
    public AudioClip coinSound;
    public AudioClip girlHit;
    public AudioClip crashObject;
    public AudioClip openDoor;
    public AudioClip getKeySound;
    public AudioClip ramenSound;
    public bool makeSound;

    //Otro personaje
    public GameObject anchorObject;

    //Movimiento
    public float speed;
    public bool orbiting;
    public Vector3 zAxis;
    public Transform posPlayer;
    private Rigidbody2D rb;
    public int distance = 1;

    //Colision con mapa
    public Tilemap tilemap_obj;
    public Sprite standingSprite;
    public Sprite roundingSprite;

   


    void Start()
    {

       transform.position = (transform.position - anchorObject.transform.position).normalized * distance + anchorObject.transform.position;
       rb = this.GetComponent<Rigidbody2D>();
       zAxis = new Vector3(0, 0, -1);
    }


    void Update()
    {
        //Orientación del sprite, siempre mirando al otro jugador
         RotateTo();

        //Gestiona giro del pj alrededor del otro
        if (orbiting == true)
        {
            transform.position = (transform.position - anchorObject.transform.position).normalized * distance + anchorObject.transform.position;
            this.GetComponent<SpriteRenderer>().sprite = roundingSprite;
            OrbitAround();
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = standingSprite;
        }
    }

    //Colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Al chocar cambiamos de dirección
        zAxis = -zAxis;

        //Posición colision
        Vector3Int tilePos = tilemap_obj.WorldToCell(posPlayer.position);

        //Colision con objetos mapa
        if (tilemap_obj.HasTile(tilePos) == true)

        {


            //FAROLILLOS
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_23" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_15")
            {
                int farolillo = PlayerPrefs.GetInt("farolillos") + 1;
                PlayerPrefs.SetInt("farolillos", farolillo);
                Player.score2 += 100;
                myAudio.PlayOneShot(crashObject);
                makeSound = true;
                Invoke("RestartMakeSound", 1);

            }
            //PORTAESPADAS o FUENTE BAMBÚ
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_18" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_17")
            {
                myAudio.PlayOneShot(crashObject);
                Player.score2 += 150;
                makeSound = true;
                Invoke("RestartMakeSound", 1);
            }
            //BONSAI
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_24" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_16")
            {
                int bonsai = PlayerPrefs.GetInt("bonsais") + 1;
                PlayerPrefs.SetInt("bonsais", bonsai);
                myAudio.PlayOneShot(crashObject);
                Player.score2 += 165;
                makeSound = true;
                Invoke("RestartMakeSound", 1);
            }

            //MONEDA
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_25" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_22")
            {
                myAudio.PlayOneShot(coinSound);
                Player.score2 += 1000;
                zAxis = -zAxis;
            }

            //LLAVE
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_26" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_23")
            {
                myAudio.PlayOneShot(getKeySound);
                Player.gotKey = true;
                zAxis = -zAxis;
            }

            //PUERTA1
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_19")
            {
                if (Player.gotKey == true)
                {
                    myAudio.PlayOneShot(openDoor);
                    PlayerPrefs.SetInt("ActualScore1", Player.score1);
                    PlayerPrefs.SetInt("ActualScore2", Player.score2);
                    SceneManager.LoadScene("Garden");
                }

            }

            //PUERTA2
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Garden_18")
            {
                if (Player.gotKey == true)
                {
                    myAudio.PlayOneShot(openDoor);
                    PlayerPrefs.SetInt("ActualScore1", Player.score1);
                    PlayerPrefs.SetInt("ActualScore2", Player.score2);
                    SceneManager.LoadScene("Boss");
                }

            }

            //RAMEN
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_58" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_24" || collision.gameObject.tag == "Ramen")
            {
                myAudio.PlayOneShot(ramenSound);
                if (Player.health2 <= 70)
                {
                    Player.health2 += 30;
                }
                else
                {
                    Player.health2 = 100;
                }
                zAxis = -zAxis;
                Player.score2 += 255;
            }

            if (tilemap_obj.GetTile(tilePos).name != "tileset_Home_19" && tilemap_obj.GetTile(tilePos).name != "tileset_Garden_18")
            {

                tilemap_obj.SetTile(tilePos, null);
            }

            //Reescanear para actualizar el grafo de A*
            AstarPath.active.Scan();

        }

        //RAMEN-BOSS
        if (collision.gameObject.tag == "ramen")
        {
            myAudio.PlayOneShot(ramenSound);
            if (Player.health2 <= 70)
            {
                Player.health2 += 30;
            }
            else
            {
                Player.health2 = 100;
            }
            Player.score2 += 255;
            Destroy(collision.gameObject);
        }

        //Colision enemigo
        if (collision.gameObject.tag == "EnemyFollower")
        {
            myAudio.PlayOneShot(girlHit);

        }
    }

    //Orientación
    void RotateTo()
    {

        Vector3 direction = anchorObject.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
    
    //Giro alrededor
    void OrbitAround()
    {
    
    transform.RotateAround(anchorObject.transform.position, zAxis, speed * Time.deltaTime);
    }

    //Sonido
    private void RestartMakeSound()
    {
        makeSound = false;
    }



}
