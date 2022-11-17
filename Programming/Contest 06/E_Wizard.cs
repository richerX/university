using System;
using System.IO;

public class Wizard : LegendaryHuman
{

    public string Rank;
    public int attackPower;
    int coeff;

    public Wizard(string name, int healthPoints, int power, string rank) : base(name, healthPoints, power)
    {
        Name = name;
        HealthPoints = healthPoints;
        Power = power;
        Rank = rank;
        switch (Rank)
        {
            case "Neophyte":
                coeff = 1;
                break;
            case "Adept":
                coeff = 2;
                break;
            case "Charmer":
                coeff = 3;
                break;
            case "Sorcerer":
                coeff = 4;
                break;
            case "Master":
                coeff = 5;
                break;
            case "Archmage":
                coeff = 6;
                break;
            default:
                throw new ArgumentException("Invalid wizard rank.");
        }
    }

    public void AttackPowerUpdate()
    {
        attackPower = Power * (int) Math.Pow(coeff, 1.5) + HealthPoints / 10;
    }

    public override void Attack(LegendaryHuman enemy)
    {
        if (this.HealthPoints > 0 && enemy.HealthPoints > 0)
        {
            AttackPowerUpdate();
            Console.WriteLine($"{this} attacked {enemy}.");
            enemy.HealthPoints -= this.attackPower;
            if (enemy.HealthPoints <= 0)
                Console.WriteLine($"{enemy} is dead.");
        }
    }

    public override string ToString()
    {
        return $"{Rank} Wizard {Name} with HP {HealthPoints}";
    }

}