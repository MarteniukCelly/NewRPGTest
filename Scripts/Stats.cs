using Godot;
using System;

public partial class Stats : Node
{
	[Export]
	public float Level = 1;

	[Export]
	public float BaseLife= 100.0f;
	protected float CurrentLife;

	[Export]
	protected float MaxLife = 100;
	[Export]
	public float BaseDamage = 20.0f;

	[Export]
	public float BaseStamina = 100.0f;

	[Export]
	public float BaseWalkSpeed = 75.0f;

	public float XP;

	[Export]
	public float LevelUpCost;

    public override void _Ready()
    {
		CurrentLife = MaxLife;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

	

}
