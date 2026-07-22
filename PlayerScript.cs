using Godot;
using System;
using System.ComponentModel;


public partial class PlayerScript : Node
{


	///Variavies
	
	[Export]
	RigidBody2D PlayerRb;

	[Export]
	AnimatedSprite2D PlayerSprite;

	[Export]
	float Speed;

	//Animation SpriteFrames

	[Export]
	SpriteFrames Idle;
	[Export]
	SpriteFrames Walk;

	
	Vector2 HorizontalInput;



	//Functios

	public override void _Ready()
	{
		
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
			PlayerSprite.SpriteFrames = Idle;            //Parado
			PlayerSprite.Play();
		}

		if(HorizontalInput != Vector2.Zero)
		{
			if(HorizontalInput.X > 0 && HorizontalInput.Y == 0 ) //andando para direita (x positivo e y =0)
			{
				GD.Print("Andando direita");
				PlayerSprite.SpriteFrames = Walk;
				PlayerSprite.FlipH = true;
				PlayerSprite.Play();
			}

			if (HorizontalInput.X < 0 && HorizontalInput.Y == 0) //Andando para esquerda (x negativo e y = 0)
			{
				GD.Print("Andando esquerda");
				PlayerSprite.SpriteFrames = Walk;
				PlayerSprite.FlipH = false;
				PlayerSprite.Play();
			}

				
		}
	}

}
