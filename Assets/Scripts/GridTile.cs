using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    Vector2 pos;
    Sprite sprite;
    SpriteRenderer spriteRenderer;
    bool walkable;
    public GridTile(Vector2 pos, Sprite sprite, SpriteRenderer spriteRenderer, bool walkable)
    {
        this.pos = pos;
        this.sprite = sprite;
        this.spriteRenderer = spriteRenderer;
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
