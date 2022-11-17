using System;

public class Program
{
    public static void Main(string[] args)
    {
        int[] array1 = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] array2 = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int i = 0;
        int j = 0;

        int answerValue = int.MaxValue;
        var answer = (0, 0);
        int current;
        while (i < array1.Length && j < array2.Length)
        {
            current = array1[i] - array2[j];
            if (Math.Abs(current) < answerValue)
            {
                answerValue = Math.Abs(current);
                answer = (array1[i], array2[j]);
            }
            if (current <= 0)
                i += 1;
            else
                j += 1;
        }

        Console.WriteLine($"{answer.Item1} {answer.Item2}");
    }
}