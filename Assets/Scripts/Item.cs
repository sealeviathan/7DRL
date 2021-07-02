using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {Weapon, Ammo, Item}
    ItemType itemType;
    public Sprite playerSprite;
    public int damage;
    private int itemIndex;
    private string itemName;
    public Item(Sprite playerSprite, ItemType itemType, int damage, int itemIndex, string itemName)
    {
        this.playerSprite = playerSprite;
        this.itemType = itemType;
        this.damage = damage;
        this.itemIndex = itemIndex;
        this.itemName = itemName;
    }
    public Item(ItemDB.ItemInfo inf)
    {
        this.playerSprite = inf.plSprite;
        this.itemType = inf.type;
        this.damage = inf.damage;
        this.itemIndex = inf.index;
        this.itemName = inf.name;
    }

    public int index
    {
        get{return this.itemIndex;}
    }
    public string name
    {
        get{return this.itemName;}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
