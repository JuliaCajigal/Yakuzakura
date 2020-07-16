using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class Orbit : MonoBehaviour
{
    //Sonidos
    public AudioSource myAudio;
    public AudioClip coinSound;
    public AudioClip crashObject;
    public AudioClip slash;
    public AudioClip openDoor;
    public AudioClip getKeySound;
    public AudioClip ramenSound;
    public AudioClip deathEnemy;


    public GameObject anchorObject;
    public float speed;
    public bool orbiting;
    public Vector3 zAxis;
    public Transform posPlayer;
    private Rigidbody2D rb;
    public int distance = 1;
    public Tilemap tilemap_obj;
    public Sprite standingSprite;
    public Sprite roundingSprite;

    public bool makeSound;





    // Start is called before the first frame update
    void Start()
    {
       // Sprite actualSprite = this.GetComponent<SpriteRenderer>().sprite;
        transform.position = (transform.position - anchorObject.transform.position).normalized * distance + anchorObject.transform.position;
        zAxis = new Vector3(0, 0, -1);
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        RotateTo();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        zAxis = -zAxis;


        Vector3Int tilePos = tilemap_obj.WorldToCell(posPlayer.position);



        //Colisiones con diferentes objetos según su nombre
        if (tilemap_obj.HasTile(tilePos) == true)

        {
            //FAROLILLOS
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_23" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_15")
            {
                int farolillo = PlayerPrefs.GetInt("farolillos") + 1;
                PlayerPrefs.SetInt("farolillos", farolillo);
                Player.score1 += 100;
                myAudio.PlayOneShot(crashObject);
                makeSound = true;
                Invoke("RestartMakeSound", 1);

            }
            //PORTAESPADAS o FUENTE BAMBÚ
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_18" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_17")
            {

                Player.score1 += 150;
                myAudio.PlayOneShot(crashObject);
                makeSound = true;
                Invoke("RestartMakeSound", 1);
            }
            //BONSAI
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_24" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_16")
            {
                int bonsai = PlayerPrefs.GetInt("bonsais") + 1;
                PlayerPrefs.SetInt("bonsais", bonsai);
                Player.score1 += 165;
                myAudio.PlayOneShot(crashObject);
                makeSound = true;
                Invoke("RestartMakeSound", 1);
            }

            //MONEDA
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_25" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_22")
            {
                myAudio.PlayOneShot(coinSound);
                Player.score1 += 1000;
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
                if(Player.gotKey == true)
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
                    SceneManager.LoadScene("Win");
                }

            }

            //RAMEN
            if (tilemap_obj.GetTile(tilePos).name == "tileset_Home_58" || tilemap_obj.GetTile(tilePos).name == "tileset_Garden_24")
            {
                myAudio.PlayOneShot(ramenSound);

                if (Player.health1 <= 70)
                {
                    Player.health1 += 30;
                }
                else
                {
                    Player.health1 = 100;
                }
                zAxis = -zAxis;
                Player.score1 += 255;
            }

            if (tilemap_obj.GetTile(tilePos).name != "tileset_Home_19" && tilemap_obj.GetTile(tilePos).name != "tileset_Garden_18")
            {

                tilemap_obj.SetTile(tilePos, null);
            }

            //Reescanear para actualizar el grafo de A*
            AstarPath.active.Scan();


        }

        //ENEMIGO DISPARADOR
        if (collision.gameObject.tag == "EnemyShooter")
        {
            int agente = PlayerPrefs.GetInt("agentes") + 1;
            PlayerPrefs.SetInt("agentes", agente);
            myAudio.PlayOneShot(slash);
            myAudio.PlayOneShot(deathEnemy);
            Player.score1 += 375;
            Destroy(collision.gameObject);
        }

        //ENEMIGO PERSEGUIDOR
        if (collision.gameObject.tag == "EnemyFollower")
        {
            myAudio.PlayOneShot(slash);
            Player.score1 += 265;
            Destroy(collision.gameObject);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

        //ENEMIGO SAMURAI
        if (collision.gameObject.tag == "EnemySamurai")
        {
            if (orbiting == true)
            {
                myAudio.PlayOneShot(slash);
                Player.score1 += 435;
                Destroy(collision.gameObject);
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }

    }
 
    void OrbitAround()
    {
    
    transform.RotateAround(anchorObject.transform.position, zAxis, speed * Time.deltaTime);
    }



    
    void RotateTo()
    {

        Vector3 direction = anchorObject.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void RestartMakeSound()
    {
        makeSound = false;
    }

}
