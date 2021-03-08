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
            mainPlayer.DoMove(Player.Actions.MoveRight);
        }
    }

}
