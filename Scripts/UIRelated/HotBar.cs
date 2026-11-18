using Godot;
using System;

public partial class HotBar : Control
{
	[Export]
	Inventory InventoryBase;

	ItemFrame[] GridsHotBar;

	[Export]
	Stats stats;

	[Export]
	HBoxContainer Hbox;

	int CurrentIndex;

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

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("Key1"))
		{
			//acessa o inventario
			CurrentIndex = 0;
		}

		if (@event.IsActionPressed("Key2"))
		{
			//acessa o inventario
			CurrentIndex = 1;
		}

		if (@event.IsActionPressed("Key3"))
		{
			//acessa o inventario
			CurrentIndex = 2;
		}

		if (@event.IsActionPressed("Key4"))
		{
			//acessa o inventario
			CurrentIndex = 3;
		}

		if (@event.IsActionPressed("Key5"))
		{
			//acessa o inventario
			CurrentIndex = 4;
		}

		if (@event.IsActionPressed("Key6"))
		{
			//acessa o inventario
			CurrentIndex = 5;
		}

		if (@event.IsActionPressed("Key7"))
		{
			//acessa o inventario
			CurrentIndex = 6;
		}

		if (@event.IsActionPressed("Key8"))
		{
			//acessa o inventario
			CurrentIndex = 7;
			
		}
	

		if (@event.IsActionPressed("UseItem"))
		{
			//Item "normal"
			//consumível
			if(InventoryBase.inventory[CurrentIndex].Item != null && InventoryBase.inventory[CurrentIndex].ItemAmount > 0)
			{
				if(InventoryBase.inventory[CurrentIndex].Item is IUsable Consumable)
				{
					Consumable.Use(stats, CurrentIndex, InventoryBase);
					GD.Print("To usando o item aqui");
				}
			}
			
		}

    }

}
