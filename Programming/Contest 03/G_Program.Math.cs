using System;

partial class Program
{
    private static double GetMin(double[] array)
    {
        return array[0];
    }

    private static double GetAverage(double[] array)
    {
        double total = 0;
        for (int i = 0; i < array.Length; i++)
        {
            total += array[i];
        }
        return total / array.Length;
    }

    private static double GetMedian(double[] array)
    {
        if (array.Length % 2 == 1)
        {
            return array[array.Length / 2];
        }
        else
        {
            return (array[array.Length / 2 - 1] + array[array.Length / 2]) / 2;
        }
    }
    
    private static double[] ReadNumbers(string line)
    {
        var array = line.Split();
        double[] answer = new double[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            answer[i] = double.Parse(array[i]);
        }
        ArraySort(ref answer);
        return answer;
    }

    public static void ArraySort(ref double[] array)
    {
        double temporary;
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[i] > array[j])
                {
                    temporary = array[i];
                    array[i] = array[j];
                    array[j] = temporary;
                }
            }
        }
    }
    
}