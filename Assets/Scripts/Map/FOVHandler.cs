using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FOVHandler
{
    static bool IsBlocking(Vector2Int pos, GridTile[,] map2D)
    {
        return map2D[pos.x,pos.y].wall;
    }
    static void SetVisible(Vector2Int pos, Queue<Vector2Int> visiblePosQueue, GridTile[,] map2D)
    {
        map2D[pos.x,pos.y].visible = true;
        visiblePosQueue.Enqueue(pos);
    }
    public static void ComputeFov(Vector2Int origin, int radius, Queue<Vector2Int> visibilityQueue, GridTile[,] map2D)
    {
        SetVisible(origin,visibilityQueue, map2D);
        Quadrant.Direction[] directions = new Quadrant.Direction[4]{Quadrant.Direction.North,Quadrant.Direction.East,Quadrant.Direction.South,Quadrant.Direction.West};

        foreach(Quadrant.Direction direction in directions)
        {
            Quadrant quadrant = new Quadrant(direction, origin);

            void Reveal(Vector2Int tilePos)
            {
                Vector2Int transformedTilePos = quadrant.TransformCoords(tilePos);
                SetVisible(transformedTilePos, visibilityQueue, map2D);
            }

            bool IsWall(Vector2Int tilePos)
            {
                if(tilePos == default(Vector2Int))
                {
                    return false;
                }
                Vector2Int transformedTilePos = quadrant.TransformCoords(tilePos);
                return IsBlocking(transformedTilePos, map2D);
            }

            bool IsFloor(Vector2Int tilePos)
            {
                return !IsWall(tilePos);
            }

            bool CheckTransform(Vector2Int tilePos,int xLength, int yLength)
            {
                Vector2Int transformedTilePos = quadrant.TransformCoords(tilePos);
                bool viable = true;
                if(transformedTilePos.x > xLength)
                    viable = false;
                if(transformedTilePos.x < 0)
                    viable = false;
                if(transformedTilePos.y > yLength)
                    viable = false;
                if(transformedTilePos.y < 0)
                    viable = false;
                return viable;
            }

            void Scan(Row row)
            {
                Vector2Int prevTile = default(Vector2Int);
                foreach(Vector2Int tilePos in row.Tiles())
                {
                    if(CheckTransform(tilePos, radius, radius))
                    {
                        if(Mathf.Abs(tilePos.x) > radius || Mathf.Abs(tilePos.y) > radius)
                            return;
                        if(IsWall(tilePos) || IsSymmetric(row, tilePos))
                            Reveal(tilePos);
                        if(IsWall(prevTile) && IsFloor(tilePos))
                            row.startSlope = Slope(tilePos);
                        if(IsFloor(prevTile) && IsWall(tilePos))
                        {
                            Row nextRow = row.Next();
                            nextRow.endSlope = Slope(tilePos);
                            Scan(nextRow);
                        }
                        prevTile = tilePos;
                    }
                }
                if(IsFloor(prevTile))
                    Scan(row.Next());
            }

        Row firstRow = new Row(1, -1f, 1f);
        Scan(firstRow);
        }
    }
    public class Quadrant
    {
        public enum Direction {North, East, South, West}
        public Direction direction;
        int originX, originY;
        public Quadrant(Direction direction, Vector2Int origin)
        {
            this.direction = direction;
            this.originX = origin.x;
            this.originY = origin.y;
        }
        public Vector2Int TransformCoords(Vector2Int tilePos)
        {
            int row = tilePos.x;
            int col = tilePos.y;
            switch(this.direction)
            {
                case Direction.North:
                    return new Vector2Int(this.originX + col, this.originY + row);
                case Direction.East:
                    return new Vector2Int(this.originX + row, this.originY + col);
                case Direction.South:
                    return new Vector2Int(this.originX + col, this.originY - row);
                case Direction.West:
                    return new Vector2Int(this.originX - row, this.originY + col);
                default:
                    Debug.LogError($"({this.direction}) Is not a valid cardinal direction");
                    break;
            }
            return default(Vector2Int);
        }
        
    }

    public class Row
    {
        public int depth;
        public float startSlope;
        public float endSlope;
        public Row(int depth, float startSlope, float endSlope)
        {
            this.depth = depth;
            this.startSlope = startSlope;
            this.endSlope = endSlope;
        }
        public IEnumerable<Vector2Int> Tiles()
        {
            int minCol = RoundUpSpecial(this.depth * this.startSlope);
            int maxCol = RoundDownSpecial(this.depth * this.endSlope);
            for(int col = minCol; col <= maxCol; col++)
            {
                yield return new Vector2Int(this.depth, col);
            }
        }
        public Row Next()
        {
            return new Row(this.depth + 1, this.startSlope, this.endSlope);
        }
    }


    static float Slope(Vector2Int tilePos)
    {
        int rowDepth = tilePos.x;
        int col = tilePos.y;
        return (2*col-1)/(2*rowDepth);
    }
    static bool IsSymmetric(Row row, Vector2Int tilePos)
    {
        int rowDepth = tilePos.x;
        int col = tilePos.y;
        return (col >= row.depth * row.startSlope && col <= row.depth * row.endSlope);
    }
    static int RoundUpSpecial(float n)
    {
        return Mathf.FloorToInt(n + 0.5f);
    }
    static int RoundDownSpecial(float n)
    {
        return Mathf.CeilToInt(n - 0.5f);
    }
}
