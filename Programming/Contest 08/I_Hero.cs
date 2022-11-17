using System;

public class Hero : Mob
{
    int initialHp;

    public Hero(int hp, int attack) : base(hp,attack)
    {
        this.hp = hp;
        this.attack = attack;
        this.initialHp = hp;
    }

    public bool IsOutOfHp()
    {
        return hp <= 0;
    }

    public bool MoreThan75()
    {
        return hp * 4 >= initialHp * 3;
    }

    public override string ToString()
    {
        return $"Mario with HP = {hp} and attack = {attack}";
    }
}

