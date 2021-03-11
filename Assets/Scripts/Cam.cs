using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public GameObject target;
    public Vector2Int relativeBounds;
    public Vector2Int[] bounds = new Vector2Int[2];
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 distance = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        Vector3 translation = new Vector3(distance.x, distance.y, 0);
        transform.Translate(translation);
        Vector2Int gridPos = GameGrid.instance.WorldToGridPos(target.transform.position);
        bounds[0] = new Vector2Int(gridPos.x - relativeBounds.x, gridPos.y + relativeBounds.y);
        bounds[1] = new Vector2Int(gridPos.x + relativeBounds.x, gridPos.y - relativeBounds.y);
    }
}
