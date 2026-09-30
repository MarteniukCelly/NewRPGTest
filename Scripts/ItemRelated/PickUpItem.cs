using Godot;
using System;

public partial class PickUpItem : Node
{
	
	[Export]
	ItemData Data;

	[Export]
	Sprite2D Sprite;

	[Export]
	Area2D Collision;

	Node2D Player;


	public override void _Ready()
	{
		Sprite.Texture = Data.ItemTextureInGame;
		Collision.BodyEntered += DetectPlayer;

	}

	
	public override void _Process(double delta)
	{
		
	}

	protected void DetectPlayer(Node body)
	{
		var bodyChildren = body.GetChildren();
		for(int i = 0; i < bodyChildren.Count; i++)
		{
			if (bodyChildren[i] is Inventory inventory)
			{
				inventory.AddItem(Data);
				GD.Print("Item adicinado");

			}
		}
	}

}
