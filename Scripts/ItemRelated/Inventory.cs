using Godot;
using System;

public partial class Inventory : Node
{
	public int InventoryMaxSlot = 12;

	public override void _Ready()
	{
		
	}

	
	public override void _Process(double delta)
	{
		
	}


	//functions: search item in inventory; put item in inventory;
	public InventorySlot[] inventory = new InventorySlot[12];

	public EquipableItem[] EquipSlots = new EquipableItem[4];

	public bool AddItem(ItemData NewItem)
	{
		SearchItemInInventory(NewItem);
		if(SearchItemInInventory(NewItem) == true)
		{
			for(int i = 0; i< inventory.Length; i++) //check null space in the slots
			{
				if(inventory[i].Item == null)
				{
					inventory[i].Item = NewItem;
					inventory[i].ItemAmount = 1;
					GD.Print($"Novo item no slot {i} quantidade total: {inventory[i].ItemAmount}");
					return false; //false for full inventory
				}
			}
		}
			return true; //true for a full inventory	
	}

	bool SearchItemInInventory(ItemData NewItem)
	{
		for(int i = 0; i < inventory.Length; i++)
		{
			if(NewItem == inventory[i].Item)
			{
				inventory[i].ItemAmount +=1;
				GD.Print($"Item stackado no slot {i} quantidade total: {inventory[i].ItemAmount}");
				return false; //False for an item that already exist

			}
		}

		return true; //true for NewItem
		
		
	}

	//Equipable item 

	public bool AddEquipableItem(EquipableItem equipable, int index)
	{
		if(EquipSlots[(int)equipable.EquipableItemSlot] == null) //caso inventário de equipable estiver sem nada 
		{
			EquipSlots[(int)equipable.EquipableItemSlot] = equipable;
			inventory[index].ItemAmount = 0;
			inventory[index].Item = null;

		}

		else
		{
			//swap the item 
			var temp = EquipSlots[(int)equipable.EquipableItemSlot];
			EquipSlots[(int)equipable.EquipableItemSlot] = equipable;				
			inventory[index].Item = temp; //Inverte pra nao ter que cair em outro caso do item diferente
			inventory[index].ItemAmount = 1;
		}

		GD.Print("You find a new placement for the item");
		GD.Print($"quem está no slot é {EquipSlots[(int)equipable.EquipableItemSlot].ItemName}");		
		return true;
	}


	
}
