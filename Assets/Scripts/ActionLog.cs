using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLog : MonoBehaviour
{
    // Start is called before the first frame update
    public static ActionLog instance;
    public int maxRecords;
    [SerializeField]
    public Queue<string> actions = new Queue<string>();
    public LightingHandler lightingHandler;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance != null)
        {
            if(instance != this)
                Destroy(this);
        }
        if(instance == this)
        {
            SingleUpdate();
        }
    }
    public void Record(string action)
    {
        if(actions.Count > maxRecords)
        {
            actions.Dequeue();
            actions.Enqueue(action);
        }
        Debug.Log(action);
        SingleUpdate();
    }
    public void SingleUpdate()
    {
        lightingHandler.DynamicLightUpdate();
    }
}
