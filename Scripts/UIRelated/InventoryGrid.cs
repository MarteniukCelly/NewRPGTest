using Godot;
using System;


public partial class InventoryGrid : GridContainer
{

	[Export]
	Inventory PlayerInventory;

	ItemFrame[] Grid; //lista de itemframes

	private ItemFrame Dragging = null;
	private Vector2 itemInitialPosition;

/// <summary>
/// Define o índice do item no inventário ao ser arrastado no início do dragging :)
/// </summary>
	int ItemInitialIndex; 
	
	private Vector2 OffSet = new Vector2(32, 32);

	public override void _Ready()
	{
		TurnSlotsChildrenToItemFrame();
	}


	public override void _Process(double delta)
	{
		 SetItemTexturesInTheInventory();
		 MouseDetection();
	}

	 void TurnSlotsChildrenToItemFrame()
	{
		
		Grid = new ItemFrame[PlayerInventory.InventoryMaxSlot];
		var children = this.GetChildren();
		for(int i = 0; i < children.Count; i++ )
		{
			Grid[i]= children[i] as ItemFrame;
		}
	}
	
	 void SetItemTexturesInTheInventory()
	{
		var InventorySlots = PlayerInventory.inventory;
		for(int i = 0 ; i < InventorySlots.Length; i++)
		{
			var Item = InventorySlots[i].Item;
			if(Item != null)
			{
				Grid[i].ItemIconTexture = Item.ItemTextureInGame;
				Grid[i].ItemAmountNumber = InventorySlots[i].ItemAmount;
			}
			else
			{
				Grid[i].ItemIconTexture = null;
				Grid[i].ItemAmountNumber = 0;
			}
		}
	}

	 void MouseDetection()
	{	
		Vector2 MousePosition = GetGlobalMousePosition();
	
		if(Input.IsActionJustPressed("LeftMouseClick"))
		{
			StartItemDrag(MousePosition);
		}
			if(Dragging != null)
			{
				Dragging.ItemIconRect.GlobalPosition = MousePosition - OffSet;	
			}

		if (Input.IsActionJustReleased("LeftMouseClick"))
		{
			EndItemDragging(MousePosition);
		}

	}

	private void StartItemDrag(Vector2 MousePosition)
	{
		GD.Print("You are initiating item drag");
		for(int i = 0; i < Grid.Length; i++)
		{
			if(Grid[i].ItemAmountNumber > 0 && Grid[i].GetGlobalRect().HasPoint(MousePosition))
			{
				GD.Print("You are currently dragging");
				Dragging = Grid[i];
				ItemInitialIndex = i;
				itemInitialPosition = Dragging.ItemIconRect.GlobalPosition;
				Dragging.ItemIconRect.ZIndex = 100;
				break;
			}
			else
			{
				GD.Print("There is no item where you pressed.");

			}
		}
		

	}

	private void EndItemDragging(Vector2 MousePosition)
	{  
		for(int i = 0; i < Grid.Length; i++)
		{
			///quando vc achou um slot pra trocar
			if(Grid[i].GetGlobalRect().HasPoint(MousePosition))
			{ 
				var temp = PlayerInventory.inventory[i];

				PlayerInventory.inventory[i] = PlayerInventory.inventory[ItemInitialIndex];
				
				PlayerInventory.inventory[ItemInitialIndex] = temp; //Inverte pra nao ter que cair em puitro caso do item diferente
				GD.Print("You find a new placement for the item");
			}


		}
		///quando não tem um slot no lugar que o mouse parou
			
		Dragging.ItemIconRect.GlobalPosition = itemInitialPosition;
		Dragging.ItemIconRect.ZIndex = 0;
		Dragging = null;
		GD.Print("You cannot fit the item there");
		
	}
}

