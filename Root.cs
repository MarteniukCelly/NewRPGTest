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


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player.OnHealthChange += UpdateHealthBar ;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
}
