using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    Player mainPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("PlayerRight"))
        {
            if(mainPlayer.DoMove(Player.Actions.MoveRight))
            {
                ActionLog.instance.Record("Moved East");
            }
        }
        else if(Input.GetButtonDown("PlayerLeft"))
        {
            if(mainPlayer.DoMove(Player.Actions.MoveLeft))
            {
                ActionLog.instance.Record("Moved West");
            }
        }
        else if(Input.GetButtonDown("PlayerUp"))
        {
            if(mainPlayer.DoMove(Player.Actions.MoveUp))
            {
                ActionLog.instance.Record("Moved North");                   
            }
        }
        else if(Input.GetButtonDown("PlayerDown"))
        {
            if(mainPlayer.DoMove(Player.Actions.MoveDown))
            {
                ActionLog.instance.Record("Moved South");               
            }
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            ActionLog.instance.StepThroughLog();
        }
        else if(Input.GetButtonDown("Pickup"))
        {
            Item curTileItem = GameGrid.instance.eMap[mainPlayer.curGridPos.x,mainPlayer.curGridPos.y].GetItem();
            Item curPlayerItem = mainPlayer.GetCurrentInventoryItem();
            //player has no item
            if(curPlayerItem == null)
            {
                if(curTileItem == null)
                {
                    //Nothing in hand or on floor, do nothing
                    return;
                }
                else
                {
                    //nothing in hand but something on floor
                    GameGrid.instance.SetTile(new Vector3Int(mainPlayer.curGridPos.x, mainPlayer.curGridPos.y, 0), default(UnityEngine.Tilemaps.TileBase), GameGrid.instance.eMapComponent);
                    GameGrid.instance.eMap[mainPlayer.curGridPos.x,mainPlayer.curGridPos.y].SetItem(null);
                    mainPlayer.SetInventoryItem(curTileItem);
                    mainPlayer.SetPlayerSprite(ItemDB.instance.plSpriteArray[curTileItem.index]);
                    ActionLog.instance.Record($"Picked up {curTileItem.name}");
                    return;

                }
            }
            else
            {
                UnityEngine.Tilemaps.TileBase curPlayerItemTile = ItemDB.instance.tileGraphicsArray[curPlayerItem.index];
                if(curTileItem == null)
                {
                    GameGrid.instance.SetTile(new Vector3Int(mainPlayer.curGridPos.x, mainPlayer.curGridPos.y, 0), curPlayerItemTile,GameGrid.instance.eMapComponent);
                    GameGrid.instance.eMap[mainPlayer.curGridPos.x,mainPlayer.curGridPos.y].SetItem(curPlayerItem);
                    mainPlayer.SetInventoryItem(null);
                    mainPlayer.ResetPlayerSprite();
                    ActionLog.instance.Record($"Put down {curPlayerItem.name}");
                    return;
                }
                UnityEngine.Tilemaps.TileBase curTileItemTile = ItemDB.instance.tileGraphicsArray[curTileItem.index];
                Sprite curPlayerItemModel = ItemDB.instance.plSpriteArray[curPlayerItem.index];
                Sprite curTileItemModel = ItemDB.instance.plSpriteArray[curPlayerItem.index];

                GameGrid.instance.eMap[mainPlayer.curGridPos.x, mainPlayer.curGridPos.y].SetItem(curPlayerItem);
                mainPlayer.SetInventoryItem(curTileItem);
                mainPlayer.SetPlayerSprite(ItemDB.instance.plSpriteArray[curTileItem.index]);
                GameGrid.instance.SetTile(new Vector3Int(mainPlayer.curGridPos.x, mainPlayer.curGridPos.y, 0), curPlayerItemTile,GameGrid.instance.eMapComponent);
                ActionLog.instance.Record($"Swapped current {curPlayerItem.name} with {curTileItem.name}");
            }
            
            //regardless of if there is an item on the current tile or not, the action we are doing is swapping.
            //either swapping nothing to something, something to something, or nothing to nothing.
            
        }
    }

}
