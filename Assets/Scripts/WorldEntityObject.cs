using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEntityObject
{
    public Sprite sprite;
    public SpriteRenderer visual;
    public Item item;
    public GameObject gameObject;
    public WorldEntityObject(Sprite sprite, Item item, Vector3 pos)
    {
        this.sprite = sprite;
        this.item = item;
        this.gameObject = GameObject.Instantiate(new GameObject("WorldEntityObject", typeof(SpriteRenderer)), pos, Quaternion.identity);
        this.visual = gameObject.GetComponent<SpriteRenderer>();
        this.visual.sprite = this.sprite;
    }

    public void SetVisualColor(Color color)
    {
        this.visual.color = color;
    }
}
