using Godot;
using System;

[GlobalClass]
public partial class EquipableItem : ItemData, IUsable
{
	public enum EquipSlots //Itens equipáveis e seus respectivos nomes 
	{
		Body,
		Weapon,
		Head,
		Shoes,
	}

	[Export]
	public EquipSlots EquipableItemSlot;

	[Export]
	public Godot.Collections.Array<StatsModifier> Modifiers = new();


    public bool Use(Stats stats, int index, Inventory inventory)
    {
       //fazer o player equipar (de acordo com o índice?)
	 	inventory.AddEquipableItem(this, index);

		stats.Recalculate(inventory.EquipSlots );

	   return true; //retornar pra n dar erro né
    }
}
