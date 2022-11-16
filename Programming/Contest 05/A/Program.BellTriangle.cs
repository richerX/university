using System;
using System.Collections.Generic;
using System.Text;

partial class Program
{
    static int[][] GetBellTriangle(uint rowCount)
    {
        var array = new int[rowCount][];
        if (rowCount > 0)
            array[0] = new int[1] { 1 };
        for (int i = 1; i < rowCount; i++)
        {
            array[i] = new int[i + 1];
            array[i][0] = array[i - 1][i - 1];
            for (int j = 1; j < i + 1; j++)
            {
                array[i][j] = array[i][j - 1] + array[i - 1][j - 1];
            }
        }
        return array;
    }

    private static void PrintJaggedArray(int[][] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length - 1; j++)
            {
                Console.Write($"{array[i][j]} ");
            }
            Console.WriteLine($"{array[i][array[i].Length - 1]}");
        }
    }
}

