using System;
using UnityEngine;

[Serializable]
public class ShopItems
{
    public string id;
    public int price;
    public string displayName;
    public bool unlocked;

    public ShopItems (string id, int price, string displayName, bool unlocked)
    {
        this.id = id;
        this.price = price;
        this.displayName = displayName;
        this.unlocked = unlocked;
    }
}
