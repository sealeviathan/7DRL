using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingHandler : MonoBehaviour
{
    public Cam cam;
    public GridLight[] lights;
    public Queue<Vector2Int> dynamicLightPositions;

    private void Start()
    {
        dynamicLightPositions = new Queue<Vector2Int>();
        GameGrid.instance.StaticBakeLightMap(lights);
    }
    public void DynamicLightUpdate()
    {
        foreach(GridLight light in lights)
        {
            if(light.lightingType == GridLight.LightMode.Dynamic)
            {
                GameGrid.instance.AddAreaDynamicLighting(light, dynamicLightPositions);
            }
        }
    }

    
}
