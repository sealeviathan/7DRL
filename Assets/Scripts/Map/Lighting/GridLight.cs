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

    private void Start()
    {
        point = new Vector2Int((int)(transform.position.x * GameGrid.instance.spacing),(int)(transform.position.y*GameGrid.instance.spacing));
        SetLightArea();
    }
    void SetLightArea()
    {
        topLeft = new Vector2Int(point.x - radius, point.y + radius);
        bottomRight = new Vector2Int(point.x + radius, point.y - radius);
    }
    void MoveLight(Vector2Int pos)
    {
        this.point = pos;
        SetLightArea();
    }
    public Vector2Int[] bounds
    {
        get{return new Vector2Int[2]{topLeft, bottomRight};}
    }
}
