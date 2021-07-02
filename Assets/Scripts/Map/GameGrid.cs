using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameGrid instance;
    public UnityEngine.Tilemaps.TileBase[] tiles;
    public UnityEngine.Tilemaps.TileBase[] entities;
    public int rows = 10;
    public int columns = 12;
    public float spacing;
    public Vector2 origin;
    public GridTile[,] map;
    public Entity[,] eMap;
    public Grid gridComponent;
    public UnityEngine.Tilemaps.Tilemap tMapComponent;
    public UnityEngine.Tilemaps.Tilemap eMapComponent;
    public float baseLightLevel = 0.0f;
    public string mapLocation;
    public string entityMapLocation;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance != null)
        {
            if(instance != this)
                Destroy(this);
        }
        if(instance == this)
        {
            transform.position = origin;
            gridComponent = gameObject.AddComponent<Grid>();
            gridComponent.cellSize = Vector2.one * spacing;
            //Build the grid component, set sizing to desired.
            tMapComponent = new GameObject("TileMapComponent").AddComponent<UnityEngine.Tilemaps.Tilemap>();
            tMapComponent.transform.position = transform.position;
            tMapComponent.transform.SetParent(this.gameObject.transform);
            //Build the tilemap component at the same position
            eMapComponent = new GameObject("EntityMapComponent").AddComponent<UnityEngine.Tilemaps.Tilemap>();
            eMapComponent.transform.position = transform.position;
            eMapComponent.transform.SetParent(this.gameObject.transform);
            //Build the entitymap component at the same position
            
            string[] lines = System.IO.File.ReadAllLines(mapLocation);
            rows = lines.Length;
            columns = lines[0].Length;
            //Map is assumed rectangle, based off the top row.
            map = new GridTile[columns,rows];
            eMap = new Entity[columns,rows];
            for(int y = 0; y < rows; y++)
            {
                string row = lines[y];
                for(int x = 0; x < columns; x++)
                {
                    int tileType = row[x] - 48;
                    //tilemap is made of numbers in positions, each number represents a position and tile type.
                    bool isWall = (int)tileType % 2 > 0;
                    //jank way of finding which tiles are nocollide
                    Vector2 offset = new Vector2(x * spacing, (rows-y-1) * spacing);
                    Vector2 pos = origin + offset;
                    
                    map[x,(rows-y-1)] = new GridTile(pos, tiles[tileType],!isWall,spacing, baseLightLevel);
                    eMap[x,(rows-y-1)] = new Entity(pos, entities[0], spacing);

                    map[x,(rows-y-1)].wall = isWall;
                    Vector3Int tilemapPos = new Vector3Int(x,(rows-y-1),0);
                    SetTile(tilemapPos, map[x,(rows-y-1)], this.tMapComponent);
                    
                }
            }
            lines = System.IO.File.ReadAllLines(entityMapLocation);
            foreach(string line in lines)
            {
                //An entitymap has multiple lines, each containing 3 numbers separated by white space, referring to an x[0] y[1] and entity[2]
                int x = 0;
                int y = 0;
                int t = 0;
                string[] pos = line.Split(' ');
                int.TryParse(pos[0],out x);
                int.TryParse(pos[1], out y);
                int.TryParse(pos[2], out t);
                SetTile(new Vector3Int(x,y,0), entities[t],eMapComponent);
                eMap[x,y].SetItem(ItemDB.instance.itemArray[t].ConvertToItem());
                

            }
            //zero out the tileset offset
            tMapComponent.tileAnchor = Vector3.zero;
            eMapComponent.tileAnchor = Vector3.zero;
            tMapComponent.transform.gameObject.AddComponent<UnityEngine.Tilemaps.TilemapRenderer>();
            eMapComponent.transform.gameObject.AddComponent<UnityEngine.Tilemaps.TilemapRenderer>().sortingOrder = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Vector2Int WorldToGridPos(Vector2 pos)
    {
        return new Vector2Int((int)((pos.x - origin.x)/spacing),(int)((pos.y - origin.y)/spacing));
    }
    public Vector2 GridToWorldPos(Vector2Int pos)
    {
        return new Vector2((pos.x + origin.x) * spacing,(pos.y + origin.y) * spacing);
    }

    public bool CheckPosInBounds(int x, int y)
    {
        if(x >= 0 && x < columns)
        {
            if(y >= 0 && y < rows)
            {
                return true;
            }
            return false;
        }
        return false;
        
    }
    public bool CheckPosInArea(int x, int y, Vector2Int topLeft, Vector2Int bottomRight)
    {
        if(!CheckPosInBounds(x,y))
            return false;
        if(x >= topLeft.x && x < bottomRight.x)
        {
            if(y >= bottomRight.y && y < topLeft.y)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    public void SetTile(Vector3Int pos, GridTile tile, UnityEngine.Tilemaps.Tilemap mapComponent)
    {
        mapComponent.SetTile(pos, tile.GetTile());
    }
    public void SetTile(Vector3Int pos, UnityEngine.Tilemaps.TileBase visual, UnityEngine.Tilemaps.Tilemap mapComponent)
    {
        mapComponent.SetTile(pos,visual);
    }
    //LIGHTING SECTION LIGHTING SECTION LIGHTING SECTION
    //Need to: Bake static lights
    //Only change all light values once
    ///<summary>Sets specific light data on a per tile basis. Range 0-1.</summary>
    public void SetTileLighting(float value, Vector3Int pos)
    {
        this.tMapComponent.SetTileFlags(pos, UnityEngine.Tilemaps.TileFlags.None);
        Color color = new Color(value,value,value);
        this.tMapComponent.SetColor(pos,color);
        if(this.map[pos.x,pos.y] != null)
        {
            
        }
    }
    void AddAreaBakedLighting(GridLight gridLight)
    {
        Vector2Int topLeft = gridLight.bounds[0];
        Vector2Int bottomRight = gridLight.bounds[1];
        for(int y = bottomRight.y; y < topLeft.y; y++)
        {
            for(int x = topLeft.x; x < bottomRight.x; x++)
            {
                if(CheckPosInBounds(x,y))
                {
                    float distLighting = Vector2.SqrMagnitude(new Vector2(gridLight.point.x - x,gridLight.point.y - y));
                    float checkRate = (gridLight.maxVal - (distLighting * gridLight.fallOffRate));
                    if(checkRate>0)
                    {
                        map[x,y].bakedLight += checkRate;
                    }
                }
            }
        }
    }
    public void AddAreaDynamicLighting(GridLight gridLight, Queue<Vector2Int> dynamicPositions)
    {
        ResetDynamicLight(dynamicPositions);
        Vector2Int topLeft = gridLight.bounds[0];
        Vector2Int bottomRight = gridLight.bounds[1];
        for(int y = bottomRight.y; y <= topLeft.y; y++)
        {
            for(int x = topLeft.x; x <= bottomRight.x; x++)
            {
                if(CheckPosInBounds(x,y))
                {
                    float distLighting = Vector2.SqrMagnitude(new Vector2(gridLight.point.x - x,gridLight.point.y - y));
                    float checkRate = (gridLight.maxVal - (distLighting * gridLight.fallOffRate));
                    if(checkRate>0.01f)
                    {
                        map[x,y].dynamicLight += checkRate;
                        dynamicPositions.Enqueue(new Vector2Int(x,y));
                        SetTileLighting(map[x,y].lighting,new Vector3Int(x,y,0));
                    }
                }
            }
        }
    }
   
    public void StaticBakeLightMap(GridLight[] lights)
    {
        //THIS SHOULD ONLY HAPPEN ONCE.
        //OR MAYBE TWICE ON ACCIDENT LOL.
        foreach(GridLight light in lights)
        {
            if(light.lightingType == GridLight.LightMode.Baked)
                AddAreaBakedLighting(light);
        }
        for(int x = 0; x < columns; x++)
        {
            for(int y = 0; y < rows; y++)
            {
                SetTileLighting(map[x,y].lighting,new Vector3Int(x,y,0));
            }
        }

    }
    public void ResetDynamicLight(Queue<Vector2Int> dynamicPositions)
    {
        while(dynamicPositions.Count > 0)
        {
            Vector2Int pos = dynamicPositions.Dequeue();
            map[pos.x,pos.y].dynamicLight = 0;
            SetTileLighting(map[pos.x,pos.y].lighting, new Vector3Int(pos.x,pos.y,0));
        }

    }
    public void CalculateFOV(Vector2Int origin, int radius, Queue<Vector2Int> visibilityQueue, bool isIterative=false)
    {
        while(visibilityQueue.Count > 0)
        {
            Vector2Int pos = visibilityQueue.Dequeue();
            map[pos.x,pos.y].visible = false;
            SetTileLighting(map[pos.x,pos.y].lighting, new Vector3Int(pos.x,pos.y,0));
        }
        FOVHandler.ComputeFov(origin, radius, visibilityQueue, map, columns,rows);
    }

    public void AddItemToMap(Vector2Int pos, Item item)
    {
        eMap[pos.x,pos.y].SetItem(item);
    }
    public Item TakeItemFromMap(Vector2Int pos)
    {
        return eMap[pos.x,pos.y].GetItem();
    }

}
