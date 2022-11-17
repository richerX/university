using System;
using System.Numerics;

public partial class Program
{
    private static BigInteger[,] GetArrayPart(int x1, int y1, int x2, int y2)
    {
        var lengths = new int[] { x2 - x1 + 1, y2 - y1 + 1 };
        var lowerBounds = new int[] { x1, y1 };
        var array = Array.CreateInstance(typeof(BigInteger), lengths, lowerBounds);
        for (int i = x1; i <= x2; i++)
        {
            for (int j = y1; j <= y2; j++)
                array.SetValue((BigInteger)i * j, i, j);
        }
        return (BigInteger[,])array;
    }
}