using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePX;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    public int numMax_dungeons;
    public int height;
    public int width;
    private SquaredDungeon mazmorra;
    public Tilemap tilemap_base;
    public Tilemap tilemap_obj;
    public TileBase[] arraytiles;

    private int[] roomCoordinates;
    public TileBase tile_obj_redLight;
    public TileBase tile_obj_bonsai;
    public TileBase tile_obj_ramen;
    public TileBase tile_obj_swords;
    public TileBase tile_obj_coin;
    public TileBase tile_obj_key;
    public TileBase tile_obj_door;
    public Vector3Int position;

    //Corrección puertas
    public TileBase tile_door_up_e;
    public TileBase tile_door_mid_e;
    public TileBase tile_door_dw_e;

    public TileBase tile_door_up_w;
    public TileBase tile_door_mid_w;
    public TileBase tile_door_dw_w;


    private ExtendedRandom rand = new ExtendedRandom();
    public int numObj_cel;
    public int numEneF_cel;
    public int numEneS_cel;
    private int rand1;
    private int rand2;

    private List<Vector3> spawnPositions = new List<Vector3>();
    private List<int[]> leavesRooms = new List<int[]>();
    private List<TileBase> objectList = new List<TileBase>();

    public GameObject enemy_follow;
    public GameObject enemy_shooter;


    // Start is called before the first frame update
    void Start()
    {

        generateDungeon();
        fillObjectList();
        changeTiles();
        spawnObjects();


    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void changeTiles()
    {

    }

    //Generate dungeon
    void generateDungeon()
    {
        mazmorra = new SquaredDungeon(height);
        mazmorra.Generate(numMax_dungeons);

        mazmorra.PaintTiles(tilemap_base, arraytiles, height, width);

        leavesRooms = mazmorra.GetLeaves();

    }

    //Genera objetos: tipo1, tipo2, llave y puerta
    void spawnObjects()
    {
        
        for (int i = 0; i < mazmorra.GetTileDescription().GetLength(0); i++)
        {
            for (int j = 0; j < mazmorra.GetTileDescription().GetLength(1); j++)
            {
                roomCoordinates = new int[] { i, j };



                if (mazmorra.HasEast(i,j))
                {
                    mazmorra.PaintElement(roomCoordinates, height, width, tilemap_base, tile_door_up_e, new int[] { 6, 9 });
                    mazmorra.PaintElement(roomCoordinates, height, width, tilemap_base, tile_door_mid_e, new int[] { 5, 9 });
                    mazmorra.PaintElement(roomCoordinates, height, width, tilemap_base, tile_door_dw_e, new int[] { 4, 9 });
                }

                if (mazmorra.HasWest(i, j))
                {
                    mazmorra.PaintElement(roomCoordinates, height, width, tilemap_base, tile_door_up_w, new int[] { 6, 0 });
                    mazmorra.PaintElement(roomCoordinates, height, width, tilemap_base, tile_door_mid_w, new int[] { 5, 0 });
                    mazmorra.PaintElement(roomCoordinates, height, width, tilemap_base, tile_door_dw_w, new int[] { 4, 0 });
                }

                if (mazmorra.GetTileDescription()[i, j] != 0)
                {
                    createEnemies(roomCoordinates);
                    createObjects(roomCoordinates);
                   
                }


            }
        }

        createKey();
        createDoor();
    }

    private void createDoor()
    {
        if (leavesRooms != null)
        {
            //Genera una puerta en una sala aleatoria de las que solo tienen una salida
            rand1 = rand.GetInt(1, leavesRooms.Count - 1) - 1;
            roomCoordinates = new int[] { leavesRooms[rand1][0], leavesRooms[rand1][1] };

            //Si la sala escogida es la [0,0]
            if (roomCoordinates[0] != 0 && roomCoordinates[1] != 0)
            {
                mazmorra.PaintElement(roomCoordinates, height, width, tilemap_obj, tile_obj_door, new int[] { height / 2, width / 2 });
            }
            else
            {
                //Mientras sea la [0][0]
                while (roomCoordinates[0] == 0 && roomCoordinates[1] == 0)
                {
                    //Cogemos una sala aleatoria entre las que tienen una sola salida
                    rand1 = rand.GetInt(1, leavesRooms.Count - 1) - 1;
                    roomCoordinates = new int[] { leavesRooms[rand1][0], leavesRooms[rand1][1] };
                }

                //Pintamos en dicha sala
                mazmorra.PaintElement(roomCoordinates, height, width, tilemap_obj, tile_obj_door, new int[] { height / 2, width / 2 });
            }
        }
    }

    private void createKey()
    {
        //Genera una llave en el último elemento de la lista de salas libres
        roomCoordinates = new int[] { leavesRooms[leavesRooms.Count-1] [0], leavesRooms[leavesRooms.Count - 1][1] };

        //Si la sala escogida es la [0,0]
        if (roomCoordinates[0] != 0 && roomCoordinates[1] != 0)
        {
            mazmorra.PaintElement(roomCoordinates, height, width, tilemap_obj, tile_obj_key, new int[] { height / 2, width / 2 });
        }
        else
        {
            //Mientras sea la [0][0]
            while (roomCoordinates[0] == 0 && roomCoordinates[1] == 0)
            {
                //Cogemos una sala aleatoria entre las que tienen una sola salida
                rand1 = rand.GetInt(1, leavesRooms.Count - 1) - 1;
                roomCoordinates = new int[] { leavesRooms[rand1][0], leavesRooms[rand1][1] };
            }

            //Pintamos la llave en esta
            mazmorra.PaintElement(roomCoordinates, height, width, tilemap_obj, tile_obj_key, new int[] { height / 2, width / 2 });
        }

 
        leavesRooms.RemoveAt(leavesRooms.Count - 1);
    }

    private void createObjects(int[] coordinates)
    {
        for (int obj = 0; obj < numObj_cel; obj++)
        {
            shuffleObjects(objectList);
            rand1 = rand.GetInt(1, width - 2);
            rand2 = rand.GetInt(1, height - 2);
            position = new Vector3Int(rand1, rand2, 0);



            if (tilemap_obj.HasTile(position) == false)
            {
                mazmorra.PaintElement(coordinates, height, width, tilemap_obj, objectList[obj], new int[] { rand1, rand2 });
            }
            else
            {
  
            }
        }

        
    }

    private void createEnemies(int[] coordinates)
    {



        for (int obj = 0; obj < numEneF_cel; obj++)
        {
            rand1 = rand.GetInt(1, width - 1);
            rand2 = rand.GetInt(1, height - 1);
            position = new Vector3Int(rand1, rand2, 0);


            if (tilemap_obj.HasTile(position) == false)
            {
                if (coordinates[0] != 0 || coordinates[1] != 0)
                {
                    position = translatePositiontoWorldCell(coordinates, position);
                    Vector3 tilePos = tilemap_obj.CellToWorld(position);
                    if (spawnPositions != null)
                    {
                        if (!spawnPositions.Contains(position))
                        {
                            Instantiate<GameObject>(enemy_follow, tilePos, Quaternion.identity);
                            spawnPositions.Add(position);
                        }
                    }
                    else
                    {
                        Instantiate<GameObject>(enemy_follow, tilePos, Quaternion.identity);
                        spawnPositions.Add(position);
                    }
                }
                else
                {

                }
            }

        }

        for (int obj = 0; obj < numEneS_cel; obj++)
        {
            rand1 = rand.GetInt(1, width - 1);
            rand2 = rand.GetInt(1, height - 1);
            Vector3Int positionShooter = new Vector3Int(rand1, rand2, 0);


            if (tilemap_obj.HasTile(positionShooter) == false && coordinates[0] != 0 || coordinates[1] != 0)
            {
                
                positionShooter = translatePositiontoWorldCell(coordinates, positionShooter);
                Vector3 tilePos = tilemap_obj.CellToWorld(positionShooter);
                tilemap_obj.GetSprite(positionShooter);

                if (spawnPositions != null)
                {
                    if (!spawnPositions.Contains(positionShooter))
                    {


                        Instantiate<GameObject>(enemy_shooter, tilePos, Quaternion.identity);
                        spawnPositions.Add(positionShooter);
                    }
                }
                else
                {
                    Instantiate<GameObject>(enemy_shooter, tilePos, Quaternion.identity);
                    spawnPositions.Add(positionShooter);
                }
            }
            else
            {

            }

        }
    }

    private Vector3Int translatePositiontoWorldCell(int[] coordinates, Vector3Int pos)
    {
        return new Vector3Int((width*coordinates[1]) + pos.x, (height*coordinates[0]) + pos.y,0);
    }


    private void fillObjectList()
    {
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_bonsai);
        objectList.Add(tile_obj_redLight);
        objectList.Add(tile_obj_redLight);
        objectList.Add(tile_obj_redLight);
        objectList.Add(tile_obj_redLight);
        objectList.Add(tile_obj_redLight);
        objectList.Add(tile_obj_redLight);
        objectList.Add(tile_obj_redLight);
        objectList.Add(tile_obj_redLight);
        objectList.Add(tile_obj_swords);
        objectList.Add(tile_obj_swords);
        objectList.Add(tile_obj_swords);
        objectList.Add(tile_obj_swords);
        objectList.Add(tile_obj_swords);
        objectList.Add(tile_obj_swords);
        objectList.Add(tile_obj_swords);
        objectList.Add(tile_obj_swords);
        objectList.Add(tile_obj_coin);
        objectList.Add(tile_obj_coin);
        objectList.Add(tile_obj_ramen);
        objectList.Add(tile_obj_ramen);

        shuffleObjects(objectList);

    }

    private void shuffleObjects(List<TileBase> objects)
    {
        System.Random rng = new System.Random();

        int n = objects.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                TileBase value = objects[k];
                objects[k] = objects[n];
                objects[n] = value;
            }
        
    }






}
