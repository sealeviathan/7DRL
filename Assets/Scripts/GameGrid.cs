using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameGrid instance;
    public Sprite[] sprites;
    int rows = 10;
    int columns = 12;
    public float spacing;
    public Vector2 origin;
    public GridTile[,] map;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance == this)
        {
            map = new GridTile[columns,rows];
            //In the near future, there will be a special python map transcripted file to pull data from.
            //For now, hardcoding :D
            for(int x = 0; x < columns; x++)
            {
                for(int y = 0; y < rows; y++)
                {
                    Vector2 offset = new Vector2(x * spacing, y * spacing);
                    Vector2 pos = origin + offset;
                    //normally would read map values for tiles
                    map[x,y] = new GridTile(pos, sprites[0],true,spacing);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckPosInBounds(int x, int y)
    {
        if(x > 0 && x <= columns)
        {
            if(y > 0 && y <= rows)
            {
                return true;
            }
            Debug.Log("Trying to move out of bounds Vertically");
            return false;
        }
        Debug.Log("Trying to move out of bounds Horizontally");
        return false;
        
    }
}
