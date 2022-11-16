using System;

public partial class Program
{
    static bool ParseArrayFromLine(string line, out int[] array)
    {
        try
        {
            array = Array.ConvertAll(line.Split(), int.Parse);
            return true;
        }
        catch
        {
            array = null;
            return false;
        }
        throw new NotImplementedException();
    }

    public static void PrintArray(int[] array)
    {
        foreach (var elem in array)
            Console.Write($"{elem} ");
        Console.WriteLine();
    }

    private static void Merge(int[] array, int left, int right, int mid)
    {
        var answer = new int[right - left];
        int answerIndex = 0;
        int i = left;
        int j = mid;
        while (i < mid || j < right)
        {
            if (i < mid && j < right)
            {
                if (array[i] < array[j])
                {
                    answer[answerIndex] = array[i];
                    i += 1;
                }
                else
                {
                    answer[answerIndex] = array[j];
                    j += 1;
                }
            }
            else
            {
                if (i < mid)
                {
                    answer[answerIndex] = array[i];
                    i += 1;
                }
                else
                {
                    answer[answerIndex] = array[j];
                    j += 1;
                }
            }
            answerIndex += 1;
        }

        /*
        PrintArray(array);
        Console.WriteLine($"left = {left}");
        Console.WriteLine($"right = {right}");
        Console.WriteLine($"mid = {mid}");
        PrintArray(answer);
        Console.WriteLine();
        */

        answerIndex = 0;
        for (int k = left; k < right; k++, answerIndex++)
            array[k] = answer[answerIndex];
    }
}