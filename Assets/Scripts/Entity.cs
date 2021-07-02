using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public UnityEngine.Tilemaps.TileBase tileVisual;
    public Vector2 pos;
    public float size;
    public Item containedItem;
    public Entity(Vector2 pos, UnityEngine.Tilemaps.TileBase tileVisual, float size)
    {
        this.pos = pos;
        this.tileVisual = tileVisual;
        this.size = size;
        this.containedItem = null;
    }

    public Item GetItem()
    {
        if(this.containedItem != null)
        {
            Item toReturn = this.containedItem;
            this.containedItem = null;
            return toReturn;
        }
        return null;
    }
    public void SetItem(Item item)
    {
        this.containedItem = item;
    }
}
