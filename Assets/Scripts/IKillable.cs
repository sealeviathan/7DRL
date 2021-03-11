using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    int health {get;set;}
    void Damage(int amount);
    void Kill();
}
