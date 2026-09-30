using Godot;
using System;

public partial class HotBar : Control
{
	[Export]
	Inventory InventoryBase;

	ItemFrame[] GridsHotBar;

	[Export]
	HBoxContainer Hbox;

	 int HotBarMaxSlots = 8;

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
			var HotBarSlots = InventoryBase.inventory;
			for(int i = 0 ; i < HotBarMaxSlots; i++)
			{
				var Item = HotBarSlots[i].Item;
				if(Item != null)
				{
					GridsHotBar[i].ItemIconTexture = Item.ItemTextureInGame;
					GridsHotBar[i].ItemAmountNumber = HotBarSlots[i].ItemAmount;
				}
				else
				{
					GridsHotBar[i].ItemIconTexture = null;
					GridsHotBar[i].ItemAmountNumber = 0;
				}
			}
		}

	 void TurnSlotsChildrenToItemFrame()
	{
		
		GridsHotBar = new ItemFrame[HotBarMaxSlots];
		var children = Hbox.GetChildren();
		for(int i = 0; i < children.Count; i++ )
		{
			GridsHotBar[i]= children[i] as ItemFrame;
		}
	}
}
