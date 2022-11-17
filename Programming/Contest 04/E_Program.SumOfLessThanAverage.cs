using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;

public partial class Program
{
    static bool IsArrayLengthCorrect(int length)
    {
        return length > 0;
    }

    static bool GenerateArray(int length, out int[] arr)
    {
        int number;
        var answer = new int[length];
        for (int i = 0; i < length; i++)
        {
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                arr = answer;
                return false;
            }
            answer[i] = number;
        }
        arr = answer;
        return true;
    }

    public static double GetArrayAverage(int[] arr)
    {
        if (arr.Length == 0)
        {
            return 0;
        }
        double totalSum = 0;
        foreach (var elem in arr)
        {
            totalSum += elem;
        }
        return totalSum / arr.Length;
    }

    public static int GetSumOfNumbersLessThanAverage(int[] arr, double average)
    {
        int answer = 0;
        foreach (var elem in arr)
        {
            if (elem < average)
            {
                answer += elem;
            }
        }
        return answer;
    }
}