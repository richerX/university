using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        var temps = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        List<int> answer = new List<int>();
        bool found;

        for (int i = 0; i < temps.Length; i++)
        {
            found = false;
            for (int j = i + 1; j < temps.Length; j++)
            {
                if (temps[j] > temps[i])
                {
                    answer.Add(j - i);
                    found = true;
                    break;
                }
            }
            if (!found)
                answer.Add(0);
        }

        for (int i = 0; i < answer.Count - 1; i++)
            Console.Write($"{answer[i]} ");
        Console.Write($"{answer[answer.Count - 1]}");
    }
}