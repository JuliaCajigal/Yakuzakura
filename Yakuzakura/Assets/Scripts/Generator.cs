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
    public int numEneSam_cel;
    private int rand1;
    private int rand2;

    private List<Vector3> spawnPositions = new List<Vector3>();
    private List<int[]> leavesRooms = new List<int[]>();
    private List<TileBase> objectList = new List<TileBase>();

    //Enemigos
    public GameObject enemy_follow;
    public GameObject enemy_shooter;
    public GameObject enemy_samurai;
    public GameObject GuidePoint;



    // Start is called before the first frame update
    void Start()
    {

        generateDungeon();
        fillObjectList();
        spawnObjects();


    }

    // Update is called once per frame
    void Update()
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


        //ENEMIGOS SERPIENTE
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

        //ENEMIGOS DISPARADORES
        for (int obj = 0; obj < numEneSam_cel; obj++)
        {
            rand1 = rand.GetInt(1, width - 1);
            rand2 = rand.GetInt(1, height - 1);
            Vector3Int positionShooter = new Vector3Int(rand1, rand2, 0);


            if (tilemap_obj.HasTile(positionShooter) == false && coordinates[0] != 0 || coordinates[1] != 0)
            {
                
                positionShooter = translatePositiontoWorldCell(coordinates, positionShooter);
                Vector3 tilePos = tilemap_obj.CellToWorld(positionShooter);
                //tilemap_obj.GetSprite(positionShooter);

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

        
        //ENEMIGOS SAMURAI
        for (int obj = 0; obj < numEneSam_cel; obj++)
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

                    //Cogemos coordenadas de la primera puerta de la sala
                    Vector3Int positionDoor1 = translatePositiontoWorldCell(coordinates, new Vector3Int(5, 9, 0));
                    Vector3 Door1Pos = tilemap_obj.CellToWorld(positionDoor1);

                    //Cogemos coordenadas de la segunda puerta de la sala
                    Vector3Int positionDoor2 = translatePositiontoWorldCell(coordinates, new Vector3Int(5, 1, 0));
                    Vector3 Door2Pos = tilemap_obj.CellToWorld(positionDoor2);

                    //Cogemos coordenadas de la tercera puerta de la sala
                    Vector3Int positionDoor3 = translatePositiontoWorldCell(coordinates, new Vector3Int(8, 5 , 0));
                    Vector3 Door3Pos = tilemap_obj.CellToWorld(positionDoor3);

                    //Cogemos coordenadas de la cuarta puerta de la sala
                    Vector3Int positionDoor4 = translatePositiontoWorldCell(coordinates, new Vector3Int(1, 5, 0));
                    Vector3 Door4Pos = tilemap_obj.CellToWorld(positionDoor4);

                    if (spawnPositions != null)
                    {
                        if (!spawnPositions.Contains(position))
                        {

                            GameObject newSamurai = Instantiate<GameObject>(enemy_samurai, tilePos, Quaternion.identity);
                            GameObject SamuraiSprite = newSamurai.transform.GetChild(1).gameObject;
                            SamuraiBehaviour newBehaviour = SamuraiSprite.GetComponent<SamuraiBehaviour>();

                            //Asignamos al Samurai los puntos guia de las puertas de su sala
                            //Puerta Norte
                            GameObject DoorPoint = Instantiate<GameObject>(GuidePoint, Door1Pos, Quaternion.identity);
                            newBehaviour.targetDoor = DoorPoint.transform;
                            newBehaviour.target = DoorPoint.transform;
                            //Puerta Sur
                            GameObject DoorPoint2 = Instantiate<GameObject>(GuidePoint, Door2Pos, Quaternion.identity);
                            newBehaviour.targetDoor2 = DoorPoint2.transform;
                            //Puerta Este
                            GameObject DoorPoint3 = Instantiate<GameObject>(GuidePoint, Door3Pos, Quaternion.identity);
                            newBehaviour.targetDoor3 = DoorPoint3.transform;
                            //Puerta Oeste
                            GameObject DoorPoint4 = Instantiate<GameObject>(GuidePoint, Door4Pos, Quaternion.identity);
                            newBehaviour.targetDoor4 = DoorPoint4.transform;

                            spawnPositions.Add(position);

                        }
                    }
                    else
                    {
                        GameObject newSamurai = Instantiate<GameObject>(enemy_samurai, tilePos, Quaternion.identity);
                        GameObject SamuraiSprite = newSamurai.transform.GetChild(1).gameObject;
                        SamuraiBehaviour newBehaviour = SamuraiSprite.GetComponent<SamuraiBehaviour>();

                        //Asignamos al Samurai los puntos guia de las puertas de su sala
                        //Puerta Norte
                        GameObject DoorPoint = Instantiate<GameObject>(GuidePoint, Door1Pos, Quaternion.identity);
                        newBehaviour.targetDoor = DoorPoint.transform;
                        newBehaviour.target = DoorPoint.transform;
                        //Puerta Sur
                        GameObject DoorPoint2 = Instantiate<GameObject>(GuidePoint, Door2Pos, Quaternion.identity);
                        newBehaviour.targetDoor2 = DoorPoint2.transform;
                        //Puerta Este
                        GameObject DoorPoint3 = Instantiate<GameObject>(GuidePoint, Door3Pos, Quaternion.identity);
                        newBehaviour.targetDoor3 = DoorPoint3.transform;
                        //Puerta Oeste
                        GameObject DoorPoint4 = Instantiate<GameObject>(GuidePoint, Door4Pos, Quaternion.identity);
                        newBehaviour.targetDoor4 = DoorPoint4.transform;

                        spawnPositions.Add(position);
                    }
                }
                else
                {

                }
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
