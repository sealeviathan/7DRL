using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;
    public int size = 1;
    public Item[] itemArray;
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
            //Make a new item for each thing, screw the scriptable thing lol
            
        }
    }

}
