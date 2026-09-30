using Godot;
using System;

public partial class ItemFrame : NinePatchRect
{

	[Export]
	public TextureRect ItemIconRect;
	public Texture2D ItemIconTexture;
									
	[Export]
	Label ItemAmountLabel;
	public int ItemAmountNumber;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ItemAmountNumber = 0; //inicia com 0 item
		ItemIconTexture = null;
	}

	public override void _Process(double delta)
	{
		SetItemInTheinventory();
	}

	public void SetItemInTheinventory()
	{
		ItemIconRect.Texture = ItemIconTexture;
		if(ItemAmountNumber <= 0)
		{
			ItemAmountLabel.Visible = false;
		}
		else
		{
			ItemAmountLabel.Text = ItemAmountNumber.ToString();
			ItemAmountLabel.Visible = true;
		}
	}

}
