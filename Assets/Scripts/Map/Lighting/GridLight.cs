using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLight: MonoBehaviour
{
    public float maxVal;
    public int radius;
    public float fallOffRate;
    public Vector2Int point;
    Vector2Int topLeft {get; set;}
    Vector2Int bottomRight {get; set;}
    public enum LightMode {Baked, Dynamic}
    public LightMode lightingType = LightMode.Baked;

    private void Start()
    {
        point = GameGrid.instance.WorldToGridPos(transform.position);
        SetLightArea();
    }
    void SetLightArea()
    {
        topLeft = new Vector2Int(point.x - radius, point.y + radius);
        bottomRight = new Vector2Int(point.x + radius, point.y - radius);
    }
    public void MoveLight(Vector2Int pos)
    {
        if(this.lightingType == LightMode.Dynamic)
        {
            this.point = pos;
            SetLightArea();
        }
        else
        {
            Debug.LogWarning("Cannot move a baked light!");
        }
    }
    public Vector2Int[] bounds
    {
        get{return new Vector2Int[2]{topLeft, bottomRight};}
    }
}
