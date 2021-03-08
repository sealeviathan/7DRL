using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 curGridPos;
    public enum Actions
    {
        MoveRight, MoveLeft, MoveUp, MoveDown
    }
    private void Start()
    {
        
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
                transform.position = wantedPos.position;
                curGridPos = new Vector2(intX + extraX, intY + extraY);
                return true;
            }
            return false;
        }
        return false;
    }
}
