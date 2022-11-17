using System;
using System.IO;

public class Knight : LegendaryHuman
{

    public int equipmentSize;
    public int attackPower;

    public Knight(string name, int healthPoints, int power, string[] equipment) : base(name, healthPoints, power)
    {
        if (equipment.Length == 0)
            throw new ArgumentException("Not enough equipment.");
        Name = name;
        HealthPoints = healthPoints;
        Power = power;
        equipmentSize = equipment.Length;
        attackPower = power + 10 * equipmentSize;
    }

    public override void Attack(LegendaryHuman enemy)
    {
        if (this.HealthPoints > 0 && enemy.HealthPoints > 0)
        {
            Console.WriteLine($"{this} attacked {enemy}.");
            enemy.HealthPoints -= (int)this.attackPower;
            if (enemy.HealthPoints <= 0)
                Console.WriteLine($"{enemy} is dead.");
        }
    }

    public override string ToString()
    {
        return $"Knight {Name} with HP {HealthPoints}";
    }
}