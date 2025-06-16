using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    MainGun,
    SubGun,
    Grenade,
    Smoke
}

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;

   
}
