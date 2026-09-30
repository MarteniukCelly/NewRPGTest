using Godot;
using System;

public partial class Root : Control
{
	[Export]
	PlayerScript Player;

	[Export]
	Label LabelLife;

	[Export]
	TextureProgressBar LifePB;

	[Export]
	Control InventoryUI;

	bool InventoryShowing = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player.OnHealthChange += UpdateHealthBar ;
		InventoryUI.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ShowInventory();
	}

	private void UpdateHealthBar(float CurrentLife)
	{
		LifePB.Value = CurrentLife;
		LabelLife.Text = $"{CurrentLife}/{LifePB.MaxValue}";
		
	}

	private void UpdateMaxBarValue(float MaxLife)
	{
		LifePB.MaxValue = MaxLife;
	}

	private void ShowInventory()
	{
		if (Input.IsActionJustPressed("InventoryToggle"))
		{
			InventoryUI.Visible = true;
			InventoryShowing = true;

		}
	}
}
