using Godot;
using System;

public abstract partial class BaseCharacter : Node
{ 
	///Export variables
	[Export]
	Stats Stats;

[ExportGroup("Bases")]
	
	[Export]
	protected float DamageAttack;
	[Export]
	protected float Speed;


[ExportGroup("Controls and Areas")]

	[Export]
	protected Area2D AttackArea;


	[Export]
	protected RigidBody2D Rb;

	//Appearence variables
[ExportGroup("Appearence")]
	
	[Export]
	protected AnimatedSprite2D Sprite;

	// Animation Sprites
[ExportGroup("Animation Sprites")]
	[Export]
	protected SpriteFrames IdleSide;
	[Export]
	protected SpriteFrames IdleFront;
	[Export]
	protected SpriteFrames IdleBack;
	[Export]
	protected SpriteFrames WalkFront;															///divide animations on three separte ones for each side
	[Export]
	protected SpriteFrames WalkSide;
	[Export]
	protected SpriteFrames WalkBack;
	[Export]
	protected SpriteFrames AttackSide;
	[Export]
	protected SpriteFrames AttackBack;
	[Export]
	protected SpriteFrames AttackFront;
	[Export]
	protected SpriteFrames DeathAnim;

	//no export variables
	protected float CurrentLife;
	protected bool IsAlive;

	protected bool IsAttacking;
	

	protected enum Side
	{
		Left,
		Right,
		Front,
		Back,
	}
	protected Side LastSide;

	//Events
	public event Action<float> OnHealthChange;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		IsAlive = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	protected void Die()
		{	
			IsAlive = false;
			Rb.Freeze = true;
			Sprite.Visible = false;

		}
	
	public void ReceiveDamage(float Damage)
	{	
		if(IsAlive == false)
		{
			return;			//protecao pro personagem n tomar dano morto
		}

		CurrentLife -= Damage;
		OnHealthChange?.Invoke(CurrentLife);
		
		if(CurrentLife <= 0)
		{
			Die();
		}
		
	}
	
}
