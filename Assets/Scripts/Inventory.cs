using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Item curItem;
    public Inventory()
    {
        this.curItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetCurItem()
    {
        return this.curItem;
    }

    public void SetCurItem(Item item)
    {
        this.curItem = item;
    }
}
