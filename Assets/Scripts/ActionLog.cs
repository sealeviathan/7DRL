using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLog : MonoBehaviour
{
    // Start is called before the first frame update
    public static ActionLog instance;
    public int maxRecords;
    [SerializeField]
    public LightingHandler lightingHandler;
    public RectTransform UIContentTransform;
    public RectTransform contentTextTransform;
    public Text contentText;
    public GameObject UIContent;
    public GameObject UIText;
    public ScrollRect scrollBar;
    public int fontSize = 14;
    int baseTextHeight;
    
    void Start()
    {
        UIContentTransform = UIContent.GetComponent<RectTransform>();
            contentTextTransform = UIText.GetComponent<RectTransform>();
            contentText = UIText.GetComponent<Text>();
            UIContentTransform.pivot = Vector2.up;
            contentTextTransform.pivot = Vector2.up;
            contentText.fontSize = fontSize;
            baseTextHeight = (int)((fontSize/10) + fontSize + 2*contentText.lineSpacing);
            contentText.text = "-START-";
        if(instance == null)
        {
            instance = this;
        }
        if(instance != null)
        {
            if(instance != this)
            {
                Debug.Log("Destroy!!");
                Destroy(this);
            }
        }
        if(instance == this)
        {
            SingleUpdate();
            
            UIContentTransform = UIContent.GetComponent<RectTransform>();
            contentTextTransform = UIText.GetComponent<RectTransform>();
            contentText = UIText.GetComponent<Text>();
            UIContentTransform.pivot = Vector2.up;
            contentTextTransform.pivot = Vector2.up;
            contentText.fontSize = fontSize;
            baseTextHeight = (int)((fontSize/10) + fontSize + 2*contentText.lineSpacing);
            contentText.text = "-START-";
            
            Debug.Log("Happened in instance");
        }
        Debug.Log("Happened at end of start");
    }
    public void Record(string action)
    {
        contentText.text += "\n" + "->" + action;
        UIContentTransform.sizeDelta = new Vector2(UIContentTransform.sizeDelta.x, UIContentTransform.sizeDelta.y + baseTextHeight);
        contentTextTransform.sizeDelta = new Vector2(contentTextTransform.sizeDelta.x,UIContentTransform.sizeDelta.y);
        contentTextTransform.anchoredPosition = new Vector2(contentTextTransform.anchoredPosition.x,UIContentTransform.sizeDelta.y/2);
        scrollBar.normalizedPosition = new Vector2(0, 0);
        SingleUpdate();
    }
    public void SingleUpdate()
    {
        lightingHandler.DynamicLightUpdate();
        
    }
    public void StepThroughLog()
    {
        lightingHandler.StepThroughLogs();
    }
}
