using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingHandler : MonoBehaviour
{
    public int FOVRange = 5;
    public Cam cam;
    public Player player;
    public GridLight[] lights;
    public Queue<Vector2Int> dynamicLightPositions;
    public Queue<Vector2Int> visibilityQueue;
    public Queue<FOVHandler.TileLogger> TileLoggerQueue;

    private void Start()
    {
        dynamicLightPositions = new Queue<Vector2Int>();
        visibilityQueue = new Queue<Vector2Int>();
        TileLoggerQueue = new Queue<FOVHandler.TileLogger>();
        GameGrid.instance.StaticBakeLightMap(lights);
    }
    public void DynamicLightUpdate()
    {
        foreach(GridLight light in lights)
        {
            if(light.lightingType == GridLight.LightMode.Dynamic)
            {
                Vector2Int playerPos = player.GetGridPos();
                
                GameGrid.instance.CalculateFOV(playerPos,FOVRange,visibilityQueue, false);
                GameGrid.instance.AddAreaDynamicLighting(light, dynamicLightPositions);
                
            }
        }
    }
    public void StepThroughLogs()
    {
        FOVHandler.TileLogger logger = TileLoggerQueue.Dequeue();
        GameGrid.instance.SetTileLighting(logger.lightLevel,new Vector3Int(logger.pos.x, logger.pos.y, 0));
        Debug.Log(logger.type);
    }

    
}
