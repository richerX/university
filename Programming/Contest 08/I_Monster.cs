using System;

public class Monster : Mob
{
    int position;

    public Monster(int hp, int attack, int position) : base(hp, attack)
    {
        this.hp = hp;
        this.attack = attack;
        this.position = position;
    }

    public void AttackHero(Mob hero, int position)
    {
        if (position == this.position)
        {
            Console.WriteLine($"Mario meet monster on {position}");
            hero.AttackMob(this);
        }
    }

    public override string ToString()
    {
        return $"Monster with HP = {hp} and attack = {attack}";
    }
}