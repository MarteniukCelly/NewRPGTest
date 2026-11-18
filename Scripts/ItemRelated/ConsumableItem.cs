using Godot;
using System;
using System.Reflection.Metadata;

[GlobalClass]
public partial class ConsumableItem : ItemData, IUsable

{

    float HealAmount = 20.00f;
    public bool Use(Stats stats, int index, Inventory inventory)
    {
        //Healing
        if (stats.CurrentLife < stats.MaxLife)
        {
            stats.Healing(HealAmount);
            return true;
        }

        return false;
    }

   
}
