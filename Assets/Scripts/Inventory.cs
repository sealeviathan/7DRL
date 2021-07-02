using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
=======
    Item curItem;
    public Inventory()
    {
        this.curItem = null;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetCurItem()
    {
        return this.curItem;
    }
}
