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

	[Export]
	Stats stats;

	bool InventoryShowing = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		stats.OnHealthChange += UpdateHealthBar ;
		InventoryUI.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ShowInventory();
	}

	private void UpdateHealthBar(float CurrentLife, float MaxLife)
	{	
		LifePB.Value = CurrentLife;
		GD.Print( $"{CurrentLife}/{MaxLife}");
		LabelLife.Text = $"{CurrentLife}/{MaxLife}";
		
		
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
