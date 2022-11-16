using System;
using System.IO;

internal class Matrix
{
    int[,] matrix;

    public Matrix(string filename)
    {
        int[] array;
        var lines = File.ReadAllLines(filename);
        matrix = new int[lines.Length, lines[0].Split(';').Length];
        SumOffEvenElements = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            array = Array.ConvertAll(lines[i].Split(';'), int.Parse);
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = array[j];
                if (array[j] % 2 == 0)
                    SumOffEvenElements += array[j];
            }
        }
    }

    public int SumOffEvenElements { get; }

    public override string ToString()
    {
        string answer = "";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1) - 1; j++)
            {
                answer += $"{matrix[i, j]}\t";
            }
            answer += $"{matrix[i, matrix.GetLength(1) - 1]}{Environment.NewLine}";
        }
        return answer;
    }
}