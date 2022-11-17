using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

partial class Program
{
    public static int[,] Transpose(int[,] matrix)
    {
        int matrixHeight = matrix.GetLength(0);
        int matrixWidth = matrix.GetLength(1);
        var newMatrix = new int[matrixWidth, matrixHeight];
        for (int i = 0; i < matrixHeight; i++)
        {
            for (int j = 0; j < matrixWidth; j++)
            {
                newMatrix[j, i] = matrix[i, j];
            }
        }
        return newMatrix;
    }

    public static int[,] Multiplication(int[,] matrixOne, int[,] matrixTwo)
    {
        int matrixOneHeight = matrixOne.GetLength(0);
        int matrixOneWidth = matrixOne.GetLength(1);
        int matrixTwoHeight = matrixTwo.GetLength(0);
        int matrixTwoWidth = matrixTwo.GetLength(1);
        var newMatrix = new int[matrixOneHeight, matrixTwoWidth];
        int value;
        for (int i = 0; i < matrixOneHeight; i++)
        {
            for (int j = 0; j < matrixTwoWidth; j++)
            {
                value = 0;
                for (int k = 0; k < matrixOneWidth; k++)
                {
                    value += matrixOne[i, k] * matrixTwo[k, j];
                }
                newMatrix[i, j] = value;
            }
        }
        return newMatrix;
    }
    
    static bool TryParseVectorFromFile(string filename, out int[] vector)
    {
        try
        {
            vector = Array.ConvertAll(File.ReadAllText(filename).Split(), int.Parse);
            return true;
        }
        catch
        {
            vector = null;
            return false;
        }
    }

    static int[,] MakeMatrixFromVector(int[] vector)
    {
        var matrix = new int[vector.Length, 1];
        for (int i = 0; i < vector.Length; i++)
        {
            matrix[i, 0] = vector[i];
        }
        return Multiplication(matrix, Transpose(matrix));
    }

    static void WriteMatrixToFile(int[,] matrix, string filename)
    {
        var lines = new string[matrix.GetLength(0)];
        string line;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            line = "";
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                line += $"{matrix[i, j] } ";
            }
            lines[i] = line;
        }
        File.WriteAllLines(filename, lines);
    }
}
