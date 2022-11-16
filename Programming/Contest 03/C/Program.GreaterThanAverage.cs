using System;

partial class Program
{
    private static int GetCountGreaterThanValue(int[] array, double average)
    {
        int answer = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > average)
            {
                answer += 1;
            }
        }
        return answer;
    }

    private static double GetAverage(int[] array)
    {
        double answer = 0;
        for (int i = 0; i < array.Length; i++)
        {
            answer += array[i];
        }
        return answer / array.Length;
    }
    
    private static bool ValidateNumber(out int n)
    {
        if (!int.TryParse(Console.ReadLine(), out n) || n < 0)
        {
            return false;
        }
        return true;
    }
    
    private static bool ReadNumbers(int n, out int[] array)
    {
        array = new int[n];
        for (int i = 0; i < n; i++)
        {
            if (!int.TryParse(Console.ReadLine(), out array[i]) || array[i] < 0)
            {
                return false;
            }
        }
        return true;
    }
}