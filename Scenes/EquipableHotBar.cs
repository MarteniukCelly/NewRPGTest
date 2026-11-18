using Godot;
using System;

public partial class EquipableHotBar : Control
{
	[Export]
	Inventory InventoryBase;

	ItemFrame[] GridsEquipHotBar;

	[Export]
	Stats stats;

	[Export]
	Control Control; //pai dos item frames

	int CurrentIndex;

	 int EquipHotBarMaxSlots = 4; 

	public override void _Ready()
	{
		TurnSlotsChildrenToItemFrame();
	}

	public override void _Process(double delta)
	{
		SetItemTexturesInTheInventory();
	}


	public void SetItemTexturesInTheInventory()
			{
				var EquipHotBarSlots = InventoryBase.EquipSlots;
				for(int i = 0 ; i < EquipHotBarMaxSlots; i++)
				{
					var Item = EquipHotBarSlots[i];
					if(Item != null) //com item 
					{
						GridsEquipHotBar[i].ItemIconTexture = Item.ItemTextureInGame;
					}
					else //sem item 
					{
						GridsEquipHotBar[i].ItemIconTexture = null;
					}
				}
			}

	
	void TurnSlotsChildrenToItemFrame()
	{
		
		GridsEquipHotBar = new ItemFrame[EquipHotBarMaxSlots];
		var children = Control.GetChildren();
		GD.Print(children.Count);
		for(int i = 0; i < children.Count; i++ )
		{
			if(children[i] is ItemFrame frame)
				GridsEquipHotBar[i]= frame;
		}
	}
 
	
}
