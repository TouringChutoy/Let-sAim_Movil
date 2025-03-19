using System;
using UnityEngine;

[Serializable]
public class ShopItems
{
    public string nameitem;
    public int moneyGame;
    public string description;
    public bool desbloquedo;

    public ShopItems (string nameitem, int moneyGame, string description, bool desbloquedo)
    {
        this.nameitem = nameitem;
        this.moneyGame = moneyGame;
        this.description = description;
        this.desbloquedo = desbloquedo;
    }
}
