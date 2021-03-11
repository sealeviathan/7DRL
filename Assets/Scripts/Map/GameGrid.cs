using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameGrid instance;
    public UnityEngine.Tilemaps.TileBase[] tiles;
    public int rows = 10;
    public int columns = 12;
    public float spacing;
    public Vector2 origin;
    public GridTile[,] map;
    public Grid gridComponent;
    public UnityEngine.Tilemaps.Tilemap tMapComponent;
    public float baseLightLevel = 0.0f;
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
            map = new GridTile[columns,rows];
            //In the near future, there will be a special python map transcripted file to pull data from.
            //For now, hardcoding :D
            for(int x = 0; x < columns; x++)
            {
                for(int y = 0; y < rows; y++)
                {
                    Vector2 offset = new Vector2(x * spacing, y * spacing);
                    Vector2 pos = origin + offset;
                    //normally would read map values for tiles
                    map[x,y] = new GridTile(pos, tiles[0],true,spacing, baseLightLevel);
                    Vector3Int tilemapPos = new Vector3Int(x,y,0);
                    SetTile(tilemapPos, map[x,y]);
                }
            }
            //zero out the tileset offset
            tMapComponent.tileAnchor = Vector3.zero;
            tMapComponent.transform.gameObject.AddComponent<UnityEngine.Tilemaps.TilemapRenderer>();
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

    public void SetTile(Vector3Int pos, GridTile tile)
    {
        this.tMapComponent.SetTile(pos, tile.GetTile());
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
        for(int y = bottomRight.y; y < topLeft.y; y++)
        {
            for(int x = topLeft.x; x < bottomRight.x; x++)
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

}
