using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Inventory inventory;
    public Vector2Int curGridPos;
    public GridLight personalLight;
    public int z_index = -1;
    public enum Actions
    {
        MoveRight, MoveLeft, MoveUp, MoveDown
    }
    private void Start()
    {
        
    }
    public Vector2Int GetGridPos()
    {
        return this.curGridPos;
    }

    public void SetInventoryItem(Item item)
    {
        inventory.SetCurItem(item);
    }

    public bool DoMove(Actions direction)
    {
        int intX = (int)curGridPos.x;
        int intY = (int)curGridPos.y;
        int extraX = 0, extraY = 0;
        switch(direction)
        {
            case Actions.MoveRight:
                extraX = 1;
                break;
            case Actions.MoveLeft:
                extraX = -1;
                break;
            case Actions.MoveUp:
                extraY = 1;
                break;
            case Actions.MoveDown:
                extraY = -1;
                break;
            default:
                Debug.LogWarning("Tried to move using a non-move based enumerator");
                return false;
        }
        if(GameGrid.instance.CheckPosInBounds(intX + extraX, intY + extraY))
        {
            GridTile wantedPos = GameGrid.instance.map[intX + extraX, intY + extraY];
            if(wantedPos.IsWalkable())
            {
                transform.position = new Vector3(wantedPos.position.x, wantedPos.position.y, z_index);
                curGridPos = new Vector2Int(intX + extraX, intY + extraY);
                personalLight.MoveLight(curGridPos);
                return true;
            }
            return false;
        }
        return false;
    }
}
