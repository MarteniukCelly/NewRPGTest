using Godot;
using System;
using System.ComponentModel;

public abstract partial class BaseEnemy : BaseCharacter
{
	[Export]
	Stats Stats;

	
	protected RigidBody2D Target;

    protected State CurrentState= State.Idling;

	[Export]
	protected Area2D TargetingArea;

	protected double TimeSinceLastAttack = 0;


	[Export]
	protected float StopDistance = 300; //distancia pra ele n ficar atravessando o player (parar antes)

	protected enum State
	{
		Walking,
		Attacking,
		Idling,
		Hurt,
		Dead,
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TargetingArea.BodyEntered += FindTarget;
		TargetingArea.BodyExited += Clear;
		CurrentState = State.Idling;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Animations();
		
	
	}

    public override void _PhysicsProcess(double delta)
    {
        ChasePlayer();
    }


	protected void ChasePlayer()
	{	

		if (Target == null)
		{
			return;
		}
		float Distance = Rb.GlobalPosition.DistanceSquaredTo(Target.GlobalPosition);
		
		if (Distance < StopDistance)
		{
			Rb.LinearVelocity = Vector2.Zero;

		}
		else
		{
			Vector2 dir = (Target.GlobalPosition - Rb.GlobalPosition).Normalized();
			Rb.LinearVelocity = dir * Speed;
			CurrentState = State.Walking;
			GD.Print(CurrentState);
		}
	}

	protected void FindTarget(Node2D body)
	{
		 var bodyChildren = body.GetChildren();
		for(int i = 0; i < bodyChildren.Count; i++)
		{
			if (bodyChildren[i] is PlayerScript player)
			{
				Target = body as RigidBody2D;

			}
		}
	}

	protected void Clear(Node2D body)
	{
			Target = null;
			Rb.LinearVelocity= Vector2.Zero;
			CurrentState = State.Idling;
			GD.Print(CurrentState);
	}

	 protected void AttackPlayer(double delta)
	{
		TimeSinceLastAttack+= delta;
		//GD.Print(TimeSinceLastAttack);
		 var bodyChildren = Target.GetChildren();
		 
		for(int i = 0; i < bodyChildren.Count; i++)
		{
			if (bodyChildren[i] is BaseCharacter Character)
			{
				Character.ReceiveDamage(DamageAttack);
				CurrentState = State.Attacking;
				GD.Print(CurrentState);
				TimeSinceLastAttack = 0;
			}
		}
	}


	protected void Animations()
	{

		if(CurrentState == State.Walking)
		{
			if(Rb.LinearVelocity.X > 0 && Rb.LinearVelocity.Y == 0)
			{
				LastSide = Side.Right;
				Sprite.SpriteFrames = WalkSide;
				Sprite.FlipH = false;
				Sprite.Play();
			}

			if(Rb.LinearVelocity.X < 0 && Rb.LinearVelocity.Y == 0)
			{
				LastSide = Side.Left;
				Sprite.SpriteFrames = WalkSide;
				Sprite.FlipH = true;
				Sprite.Play();
			}

			if(Rb.LinearVelocity.X ==0 && Rb.LinearVelocity.Y > 0)
			{
				LastSide = Side.Front;
				Sprite.SpriteFrames = WalkFront;
					Sprite.Play();
			}

			if(Rb.LinearVelocity.X ==0 && Rb.LinearVelocity.Y < 0)
			{	
				LastSide = Side.Back;
				Sprite.SpriteFrames = WalkBack;
				Sprite.Play();
			}


		}
	}

}

	

