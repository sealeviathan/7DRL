using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile
{
    Vector2 pos;
    Sprite sprite;
    SpriteRenderer spriteRenderer;
    GameObject self;
    string name;
    float size;
    bool walkable;
    public GridTile(Vector2 pos, Sprite sprite, bool walkable, float size)
    {
        this.pos = pos;
        this.size = size;
        this.name = $"GRIDMAP:{pos.x/size},{pos.y/size}";
        this.self = new GameObject(this.name);
        this.self.transform.position = pos;
        this.self.AddComponent<SpriteRenderer>();
        this.spriteRenderer = this.self.GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = sprite;
        this.walkable = walkable;

    }

    public bool IsWalkable()
    {
        return this.walkable;
    }
    public Vector2 position
    {
        get{return this.pos;}
    }

    
}
