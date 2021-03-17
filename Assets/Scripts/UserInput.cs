using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    Player mainPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("PlayerRight"))
        {
            if(mainPlayer.DoMove(Player.Actions.MoveRight))
            {
                ActionLog.instance.Record("Moved East");
            }
        }
        else if(Input.GetButtonDown("PlayerLeft"))
        {
            if(mainPlayer.DoMove(Player.Actions.MoveLeft))
            {
                ActionLog.instance.Record("Moved West");
            }
        }
        else if(Input.GetButtonDown("PlayerUp"))
        {
            if(mainPlayer.DoMove(Player.Actions.MoveUp))
            {
                ActionLog.instance.Record("Moved North");                   
            }
        }
        else if(Input.GetButtonDown("PlayerDown"))
        {
            if(mainPlayer.DoMove(Player.Actions.MoveDown))
            {
                ActionLog.instance.Record("Moved South");               
            }
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            ActionLog.instance.StepThroughLog();
        }
    }

}
