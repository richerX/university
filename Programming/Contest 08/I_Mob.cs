using System;

public abstract class Mob
{
    protected int hp;
    protected int attack;

    public Mob(int hp, int attack)
    {
        this.hp = hp;
        this.attack = attack;
    }
    
    public void AttackMob(Mob other)
    {
        while (hp > 0 && other.hp > 0)
        {
            Console.WriteLine(ToString() + " attacked " + other.ToString());
            Console.WriteLine(other.ToString() + " attacked " + ToString());
            hp -= other.attack;
            other.hp -= attack;
        }
    }

    public override string ToString()
    {
        return $"Mob with HP = {hp} and attack = {attack}";
    }
}