using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile
{
    Vector2 pos;
    string name;
    float size;
    bool walkable;
    bool isWall;
    bool isVisible;
    float bakedLightValue;
    float dynamicAddedLightValue;
    UnityEngine.Tilemaps.TileBase tile;
    public GridTile(Vector2 pos, UnityEngine.Tilemaps.TileBase tile, bool walkable, float size, float startingLighting)
    {
        this.pos = pos;
        this.size = size;
        this.name = $"GRIDMAP:{pos.x/size},{pos.y/size}";
        this.walkable = walkable;
        this.tile = tile;
        this.bakedLightValue = startingLighting;
        this.dynamicAddedLightValue = 0;
        this.isVisible = false;
        this.isWall = false;
    }

    public bool IsWalkable()
    {
        return this.walkable && !this.isWall;
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
        get
        {
            if(this.isVisible)
                return this.bakedLightValue + this.dynamicAddedLightValue;
            else
                return 0;
        }
    }
    public float bakedLight {get{return this.bakedLightValue;} set{this.bakedLightValue = value;}}
    public float dynamicLight {get{return this.dynamicAddedLightValue;} set{this.dynamicAddedLightValue = value;}}
    public bool wall {get{return this.isWall;} set{this.isWall = value;}}
    public bool visible {get{return this.isVisible;} set{this.isVisible = value;}}

    
}
