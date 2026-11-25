using Godot;
using System;
using System.Dynamic;
using System.Reflection.Metadata;

[GlobalClass]
public partial class ConsumableItem : ItemData, IUsable

{
    public Godot.Collections.Array<StatsModifier> SideEffect{get; private set;} = new();

    
    public bool Use(Stats stats, int index, Inventory inventory)
        {   
            bool Used = false;
            bool HealthFull = false;
          foreach (StatsModifier Stat in SideEffect)
        {
            if(Stat.StatType == StatType.MaxLife ) //modifier de max life 
            {
                stats.MaxLife += Stat.Amount;
            }
            if(Stat.StatType == StatType.CurrentLife) //mod de currentlife
            {
                stats.CurrentLife += Stat.Amount;
                if(stats.CurrentLife == stats.MaxLife)
                {
                    Used = true;
                    HealthFull = true;
                }
            }
            if(Stat.StatType == StatType.Damage) //mod de dano
            {
                stats.BaseDamage += Stat.Amount;
            }
            if(Stat.StatType == StatType.Speed) // mod de velocidade
            {
                stats.BaseWalkSpeed += Stat.Amount;
            }
        }
            return Used;
        }

    

}
