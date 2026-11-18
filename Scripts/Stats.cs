using Godot;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

public partial class Stats : Node
{
	[Export]
	public float Level = 1;

	[Export]
	public float BaseLife= 100.0f;
	
	public float CurrentLife;

	[Export]
	public float MaxLife = 100;

	[Export]
	public float BaseDamage = 20.0f;

	[Export]
	public float BaseStamina = 100.0f;

	[Export]
	public float BaseWalkSpeed = 75.0f;

	public float XP;

	[Export]
	public float LevelUpCost;


	// Events 

	public event Action<float, float> OnHealthChange;

    public override void _Ready()
    {
		CurrentLife = MaxLife;
        base._Ready();
		
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

	public void Healing(float HealAmount)
	{
		if(HealAmount > 0) // recuperar vida
		{
			CurrentLife += HealAmount;

			
		}
		
	}

	public void Recalculate(EquipableItem[] equipped)
	{
		float BonusMaxLife=0;
		float BonusDamage=0;
		float BonusSpeed=0;

		for (int i =0; i < equipped.Length; i++)
		{

			
			if (equipped[i] == null) // se n tiver equipado
			{
				continue;
			}

			foreach (var mod in equipped[i].Modifiers) //ver o q ele ta mudando
			{
				if (mod.StatType == StatType.MaxLife) //HP
				{
					BonusMaxLife = mod.Amount;
				}

				if (mod.StatType == StatType.Damage) //DMG
				{
					BonusDamage  = mod.Amount;
				}

				if (mod.StatType == StatType.Speed) //Speed
				{
					BonusSpeed  = mod.Amount;
				}
			}

			//Mudar HP, speed e dmg

			MaxLife += BonusMaxLife;
			BaseDamage += BonusDamage;
			BaseWalkSpeed += BonusSpeed;
			OnHealthChange?.Invoke(this.CurrentLife, this.MaxLife);
		}
		
	}


	public void UpdateCurrentLife(float Damage) //variacao da funcao Recieve damage do base character pra n dar conflito 
	{
		CurrentLife -= Damage;
		OnHealthChange?.Invoke(this.CurrentLife, this.MaxLife);
	}
}
