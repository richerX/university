using System;

public delegate void AtackHeroOnPosition(Mob hero, int position);

public class Game
{
    int castlePosition;
    int countOfMonsters;
    Hero hero;
    Boss boss;
    public AtackHeroOnPosition attackHero;

    public Game(int castlePosition, int countOfMonster, Hero hero, Boss boss)
    {
        this.castlePosition = castlePosition;
        this.countOfMonsters = countOfMonster;
        this.hero = hero;
        this.boss = boss;
    }
    public void Play()
    {
        bool lost = false;
        for (int curPosition = 1; curPosition < castlePosition + 1; curPosition++)
        {
            if (countOfMonsters > 0)
                attackHero(hero, curPosition);
            if (hero.IsOutOfHp())
            {
                Console.WriteLine("You Lose! Game over!");
                lost = true;
                break;
            }
        }

        if (!lost)
        {
            hero.AttackMob(boss);
            if (hero.IsOutOfHp())
                Console.WriteLine("You Lose! Game over!");
            else if (hero.MoreThan75())
                Console.WriteLine("You win! Princess released!");
            else
                Console.WriteLine("Thank you, Mario! But our princess is in another castle... You lose!");
        }
    }
}
