using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile
{
    Vector2 pos;
    string name;
    float size;
    bool walkable;
    float lightValue;
    UnityEngine.Tilemaps.TileBase tile;
    public GridTile(Vector2 pos, UnityEngine.Tilemaps.TileBase tile, bool walkable, float size)
    {
        this.pos = pos;
        this.size = size;
        this.name = $"GRIDMAP:{pos.x/size},{pos.y/size}";
        this.walkable = walkable;
        this.tile = tile;
        this.lightValue = 1;
    }

    public bool IsWalkable()
    {
        return this.walkable;
    }
    public Vector2 position
    {
        get{return this.pos;}
    }
    public UnityEngine.Tilemaps.TileBase GetTile()
    {
        return this.tile;
    }
    public float lighting
    {
        get{return this.lightValue;}
        set{this.lightValue = value;}
    }

    
}
