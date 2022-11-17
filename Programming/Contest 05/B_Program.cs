using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

internal static class Program
{
    public static int[] ParseInput(string input)
    {
        return Array.ConvertAll(input.Split(","), int.Parse);
    }

    public static int[][] CreatePermutations(int[] firstRow)
    {
        int length = firstRow.Length;
        var array = new int[length][];
        array[0] = firstRow;
        for (int i = 1; i < length; i++)
        {
            array[i] = new int[length];
            for (int j = 0; j < length; j++)
            {
                array[i][j] = array[i-1][(j+1)%length];
            }
        }
        return array;
    }

    private static void PrintArray(int[][] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length - 1; j++)
            {
                Console.Write($"{array[i][j]}");
            }
            Console.WriteLine($"{array[i][array[i].Length - 1]}");
        }
    }

    private static void Main(string[] args)
    {
        var array = ParseInput(Console.ReadLine());
        var permutations = CreatePermutations(array);
        PrintArray(permutations);
    }
}