using Godot;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Transactions;


public partial class PlayerScript : BaseCharacter

{

	///Variavies
	
	Vector2 HorizontalInput;


	//Functions
	public override void _Ready()
	{
		base._Ready();
	}

	
	public override void _Process(double delta)
	{
		Movements(delta);
		Animations();
		RotateAttackArea();
		
	}


	public void Movements(double delta)
	{
		HorizontalInput = Input.GetVector("Left", "Right","Up","Down"); // creating vector for player input
		Rb.LinearVelocity= HorizontalInput * this.Speed; //Adding the velocity to make player walk
		
	}


	private void Animations()
	{
		if(HorizontalInput == Vector2.Zero) 
		{
			if(Sprite.SpriteFrames == WalkFront || Sprite.SpriteFrames == AttackFront) //Idle front
			{
				LastSide= Side.Front;
				Sprite.SpriteFrames = IdleFront;
				Sprite.Play();
			}

			if(Sprite.SpriteFrames == WalkSide || Sprite.SpriteFrames == AttackSide) 
			{
				
				Sprite.SpriteFrames = IdleSide;
				Sprite.Play();
			}

			if(Sprite.SpriteFrames == WalkBack || Sprite.SpriteFrames == AttackBack)
			{
				LastSide= Side.Back;
				Sprite.SpriteFrames = IdleBack;
				Sprite.Play();

			}

		}

			if(HorizontalInput != Vector2.Zero) //Animations walk
			{
				if(HorizontalInput.X > 0 && HorizontalInput.Y == 0 ) //andando para direita (x positivo e y =0)
				{
					LastSide= Side.Right;
					Sprite.SpriteFrames = WalkSide;
					Sprite.FlipH = false;
					Sprite.Play();
				}

				if (HorizontalInput.X < 0 && HorizontalInput.Y == 0) //Andando para esquerda (x negativo e y = 0)
				{
					LastSide= Side.Left;
					Sprite.SpriteFrames = WalkSide;
					Sprite.FlipH = true;
					Sprite.Play();
				}

				if(HorizontalInput.X ==0 && HorizontalInput.Y > 0) //walking front
				{	
					LastSide= Side.Front;
					Sprite.SpriteFrames = WalkFront;
					Sprite.Play();
				}

				if(HorizontalInput.X ==0 && HorizontalInput.Y < 0) //walking back
				{
					LastSide= Side.Back;
					Sprite.SpriteFrames = WalkBack;
					Sprite.Play();
				}
			}
		

		
	}

	public void RotateAttackArea()
	{
		if (LastSide == Side.Front)
		{
			AttackArea.RotationDegrees= 0;
			

		}

		if (LastSide == Side.Back)
		{
			AttackArea.RotationDegrees= 180;
			
		}

		if (LastSide == Side.Left)
		{
			AttackArea.RotationDegrees= 90;
			

		}

		if (LastSide == Side.Right)
		{
			AttackArea.RotationDegrees= 270;
			
		}


	}

	public void Attack(float DamageAttack)
	{
		IsAttacking = true;

	}

	public void AttackEnd()
	{
		
	}
	
}
