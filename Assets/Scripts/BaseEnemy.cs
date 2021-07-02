using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IKillable
{
    int baseHealth;
    int baseDamage;
    public enum UnguidedMoveDir {North, East, South, West};
    public UnguidedMoveDir curRandomMoveDir;
    public int health { get {return this.baseHealth;}}
    public int damage { get {return this.baseHealth;}}
    SpriteRenderer spriteRenderer;

    public Vector2Int curGridPos;

    public void Damage(int amount)
    {
        this.baseHealth -= amount;
        if(this.baseHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        curRandomMoveDir = ChooseRandomDirection();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 2;
    }
    UnguidedMoveDir ChooseRandomDirection()
    {
        switch(Random.Range(0,4))
        {
            case 0:
                return UnguidedMoveDir.North;
            case 1:
                return UnguidedMoveDir.East;
            case 2:
                return UnguidedMoveDir.South;
            default:
                return UnguidedMoveDir.West;
        }
    }

    public void MoveRandom()
    {
        DoMove(ChooseRandomDirection());
    }
    public bool DoMove(UnguidedMoveDir direction)
    {
        int intX = (int)curGridPos.x;
        int intY = (int)curGridPos.y;
        int extraX = 0, extraY = 0;
        switch(direction)
        {
            case UnguidedMoveDir.East:
                extraX = 1;
                break;
            case UnguidedMoveDir.West:
                extraX = -1;
                break;
            case UnguidedMoveDir.North:
                extraY = 1;
                break;
            case UnguidedMoveDir.South:
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
                transform.position = new Vector3(wantedPos.position.x, wantedPos.position.y, 2);
                curGridPos = new Vector2Int(intX + extraX, intY + extraY);
                float curPosLightLevel = GameGrid.instance.map[curGridPos.x, curGridPos.y].lighting;
                spriteRenderer.color = new Color(curPosLightLevel, curPosLightLevel, curPosLightLevel);
                return true;
            }
            return false;
        }
        return false;
    }

    
    public void DoUpdate()
    {
        MoveRandom();
    }
}
