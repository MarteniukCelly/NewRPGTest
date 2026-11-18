using Godot;
using System;



public enum StatType
{
	MaxLife,
	Damage,
	Speed
}

[GlobalClass]
public partial class StatsModifier : Resource
{
	[Export]
	public StatType StatType;
	[Export]
	public float Amount;
}

