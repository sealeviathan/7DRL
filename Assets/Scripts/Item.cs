using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public bool isWeapon;
    public bool isAmmo;
    public bool isItem;

}
