using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingHandler : MonoBehaviour
{
    public Cam cam;
    public Player player;
    public GridLight[] lights;
    public Queue<Vector2Int> dynamicLightPositions;
    public Queue<Vector2Int> visibilityQueue;

    private void Start()
    {
        dynamicLightPositions = new Queue<Vector2Int>();
        visibilityQueue = new Queue<Vector2Int>();
        GameGrid.instance.StaticBakeLightMap(lights);
    }
    public void DynamicLightUpdate()
    {
        foreach(GridLight light in lights)
        {
            if(light.lightingType == GridLight.LightMode.Dynamic)
            {
                Vector2Int playerPos = player.GetGridPos();
                
                GameGrid.instance.CalculateFOV(playerPos,5,visibilityQueue,false);
                GameGrid.instance.AddAreaDynamicLighting(light, dynamicLightPositions);
                
            }
        }
    }

    
}
