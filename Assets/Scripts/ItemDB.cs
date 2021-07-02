using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public class ItemInfo
    {
        private Item.ItemType itemType;
        private Sprite playerSprite;
        private UnityEngine.Tilemaps.TileBase tileGraphics;
        private int itemDamage;
        private int itemIndex;
        private string itemName;
        public ItemInfo(Item.ItemType itemType, Sprite playerSprite, UnityEngine.Tilemaps.TileBase tileGraphics, int itemDamage, int itemIndex, string itemName)
        {
            this.itemType = itemType;
            this.playerSprite = playerSprite;
            this.tileGraphics = tileGraphics;
            this.itemDamage = itemDamage;
            this.itemIndex = itemIndex;
            this.itemName = itemName;
        }

        public int index
        {
            get{return this.itemIndex;}
        }
        public string name
        {
            get{return this.itemName;}
        }
        public Item.ItemType type
        {
            get{return this.itemType;}
        }
        public Sprite plSprite
        {
            get{return this.playerSprite;}
        }
        public UnityEngine.Tilemaps.TileBase tile
        {
            get{return this.tileGraphics;}
        }
        public int damage
        {
            get{return this.itemDamage;}
        }
        public Item ConvertToItem()
        {
            return new Item(this.playerSprite, this.itemType, this.itemDamage, this.itemIndex, this.itemName);
        }
    }
    public static ItemDB instance;
    public int size = 1;
    public ItemInfo[] itemArray;
    public Sprite[] plSpriteArray;
    public UnityEngine.Tilemaps.TileBase[] tileGraphicsArray;
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
            itemArray = new ItemInfo[size];
            //Make a new item for each thing, screw the scriptable thing lol
            itemArray[0] = new ItemInfo(Item.ItemType.Item,plSpriteArray[0],tileGraphicsArray[0], 0,0,"key");
            itemArray[1] = new ItemInfo(Item.ItemType.Weapon,plSpriteArray[1],tileGraphicsArray[1], 5,1,"wepon");
            
        }
    }

}
