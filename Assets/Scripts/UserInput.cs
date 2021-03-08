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
            Debug.Log(mainPlayer.DoMove(Player.Actions.MoveRight));
        else if(Input.GetButtonDown("PlayerLeft"))
            mainPlayer.DoMove(Player.Actions.MoveLeft);
        else if(Input.GetButtonDown("PlayerUp"))
            mainPlayer.DoMove(Player.Actions.MoveUp);
        else if(Input.GetButtonDown("PlayerDown"))
            mainPlayer.DoMove(Player.Actions.MoveDown);

    }

}
