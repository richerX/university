using System;
using System.Collections.Generic;

public class Game
{
    public static List<Mario> helpers = new List<Mario>();
    public static int numberOfPlayedRounds = 0;

    public int numberOfHeroes;
    public int numberOfPlumbers;
    public int numberOfMarios;
    public int numberOfRounds;

    public int kills => numberOfHeroes + numberOfMarios;
    public int fixes => numberOfPlumbers + numberOfMarios;

    public Game(int numberOfHeroes, int numberOfPlumbers, int numberOfMarios, int numberOfRounds)
    {
        this.numberOfHeroes = numberOfHeroes;
        this.numberOfPlumbers = numberOfPlumbers;
        this.numberOfMarios = numberOfMarios;
        this.numberOfRounds = numberOfRounds;
    }

    public void Play()
    {
        int monsters, pipes;
        for (int i = 0; i < numberOfRounds; i++)
        {
            var answer = readRound();
            monsters = answer.Item1;
            pipes = answer.Item2;

            if (kills < monsters && fixes < pipes)
            {
                Console.WriteLine("Helpers lost this round!");
                numberOfMarios += 1;
            }

            else if (kills < monsters)
            {
                Console.WriteLine("Helpers lost this round!");
                numberOfHeroes += 1;
            }

            else if (fixes < pipes)
            {
                Console.WriteLine("Helpers lost this round!");
                numberOfPlumbers += 1;
            }

            else
                Console.WriteLine("Helpers won this round!");
        }

        int sum = numberOfHeroes + numberOfPlumbers + numberOfMarios;
        for (int j = 0; j < sum; j++)
            helpers.Add(new Mario());
    }

    public static Tuple<int, int> readRound()
    {
        int monsters, pipes;
        while (true)
        {
            string[] array = Console.ReadLine().Split();
            if (array.Length == 2 && int.TryParse(array[0], out monsters) && int.TryParse(array[1], out pipes))
                return Tuple.Create(monsters, pipes);
            Console.WriteLine("Incorrect data!");
        }
    }
}