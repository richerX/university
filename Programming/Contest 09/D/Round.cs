using System;

public class Round
{
    private int monsters;
    private int pipes;

    public Round(int amountOfMonsters, int amountOfCrashes)
    {
        monsters = amountOfMonsters;
        pipes = amountOfCrashes;
    }

    public void Play(Game game)
    {
        Console.WriteLine(game.kills);
    }
}