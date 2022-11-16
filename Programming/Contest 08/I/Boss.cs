public class Boss : Mob
{
    public Boss(int hp, int attack) : base(hp,attack) 
    {
        this.hp = hp;
        this.attack = attack;
    }

    public override string ToString()
    {
        return $"Boss with HP = {hp} and attack = {attack}";
    }
}
