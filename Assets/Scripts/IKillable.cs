using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    int health {get;}
    void Damage(int amount);
    void Kill();
}
