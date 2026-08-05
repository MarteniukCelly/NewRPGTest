using Godot;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Transactions;


public partial class PlayerScript : Node
{
	///Variavies
	
	[Export]
	Stats Stats;

	[ExportGroup("PlayerBases")]
	[Export]
	float MaxLife = 100;
	float CurrentLife;
	bool IsAlive;
	[Export]
	float Speed;


	[Export]
	RigidBody2D PlayerRb;

	[Export]
	AnimatedSprite2D PlayerSprite;

	Vector2 HorizontalInput;
	public event Action<float> OnHealthChange;

	//Animation SpriteFrames

	// Animation Sprites
	[ExportGroup("Animation Sprites")]
	[Export]
	SpriteFrames IdleSide;
	[Export]
	SpriteFrames IdleFront;
	[Export]
	SpriteFrames IdleBack;
	[Export]
	SpriteFrames WalkFront;															///divide animations on three separte ones for each side
	[Export]
	SpriteFrames WalkSide;
	[Export]
	SpriteFrames WalkBack;
	[Export]
	SpriteFrames AttackSide;
	[Export]
	SpriteFrames AttackBack;
	[Export]
	SpriteFrames AttackFront;
	[Export]
	SpriteFrames DeathAnim;

	//Functions
	public override void _Ready()
	{
		CurrentLife = MaxLife;
		IsAlive = true;
		
	}

	
	public override void _Process(double delta)
	{
		Movements(delta);
		Animations();
	}


	public void Movements(double delta)
	{
		HorizontalInput = Input.GetVector("Left", "Right","Up","Down"); // creating vector for player input
		PlayerRb.LinearVelocity= HorizontalInput * this.Speed; //Adding the velocity to make player walk
		GD.Print(HorizontalInput);
	}


	private void Animations()
	{
		if(HorizontalInput == Vector2.Zero) 
		{
			if(PlayerSprite.SpriteFrames == WalkFront || PlayerSprite.SpriteFrames == AttackFront) //Idle front
			{
				PlayerSprite.SpriteFrames = IdleFront;
				PlayerSprite.Play();
			}

			if(PlayerSprite.SpriteFrames == WalkSide || PlayerSprite.SpriteFrames == AttackSide) 
			{
				PlayerSprite.SpriteFrames = IdleSide;
				PlayerSprite.Play();
			}

			if(PlayerSprite.SpriteFrames == WalkBack || PlayerSprite.SpriteFrames == AttackBack)
			{
				PlayerSprite.SpriteFrames = IdleBack;
				PlayerSprite.Play();
			}

			if(HorizontalInput != Vector2.Zero) //Animations walk
			{
				if(HorizontalInput.X > 0 && HorizontalInput.Y == 0 ) //andando para direita (x positivo e y =0)
				{
					PlayerSprite.SpriteFrames = WalkSide;
					PlayerSprite.FlipH = true;
					PlayerSprite.Play();
				}

				if (HorizontalInput.X < 0 && HorizontalInput.Y == 0) //Andando para esquerda (x negativo e y = 0)
				{
					PlayerSprite.SpriteFrames = WalkSide;
					PlayerSprite.FlipH = false;
					PlayerSprite.Play();
				}

				if(HorizontalInput.X ==0 && HorizontalInput.Y > 0) //walking front
				{	
					PlayerSprite.SpriteFrames = WalkFront;
					PlayerSprite.Play();
				}

				if(HorizontalInput.X ==0 && HorizontalInput.Y < 0) //walking back
				{
					PlayerSprite.SpriteFrames = WalkBack;
					PlayerSprite.Play();
				}
			}
		}

		
	}

	public void ReceiveDamage(float Damage)
	{	
		if(IsAlive == false)
		{
			return;			//protecao pro player n tomar dano morto
		}

		CurrentLife -= Damage;
		OnHealthChange?.Invoke(CurrentLife);
		
		if(CurrentLife <= 0)
		{
			Die();
		}
		
	}

	private void Die()
	{	
		IsAlive = false;
		PlayerRb.Freeze = true;
		PlayerSprite.Visible = false;

	}
	
}
