using System;
using System.Collections.Generic;
using System.Linq;

class Polynom
{

    public static bool TryParsePolynom(string line, out int[] array)
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
    }

    public static int[] Sum(int[] a, int[] b)
    {
        var array = new int[Math.Max(a.Length, b.Length)];
        for (int i = 0; i < Math.Max(a.Length, b.Length); i++)
        {
            if (i < a.Length)
                array[i] += a[i];
            if (i < b.Length)
                array[i] += b[i];
        }
        return array;
    }

    public static int[] Dif(int[] a, int[] b)
    {
        var array = new int[Math.Max(a.Length, b.Length)];
        for (int i = 0; i < Math.Max(a.Length, b.Length); i++)
        {
            if (i < a.Length)
                array[i] += a[i];
            if (i < b.Length)
                array[i] -= b[i];
        }
        return array;
    }

    public static int[] MultiplyByNumber(int[] a, int n)
    {
        var array = new int[a.Length];
        for (int i = 0; i < a.Length; i++)
            array[i] = a[i] * n;
        return array;
    }

    public static int[] MultiplyByPolynom(int[] a, int[] b)
    {
        var array = new int[a.Length + b.Length];
        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                array[i + j] += a[i] * b[j];
            }
        }
        return array;
    }

    public static string PolynomToString(int[] polynom)
    {
        var lines = new List<string>();
        for (int i = polynom.Length - 1; i > -1; i--)
        {
            if (i > 1)
            {
                if (polynom[i] == 1)
                    lines.Add($"x{i}");
                if (polynom[i] != 0 && polynom[i] != 1)
                    lines.Add($"{polynom[i]}x{i}");
            }
            if (i == 1)
            {
                if (polynom[i] == 1)
                    lines.Add($"x");
                if (polynom[i] != 0 && polynom[i] != 1)
                    lines.Add($"{polynom[i]}x");
            }
            if (i == 0)
            {
                if (polynom[i] != 0)
                    lines.Add($"{polynom[i]}");
            }
        }
        if (lines.Count == 0)
            return ("0");
        return string.Join(" + ", lines);
    }
}
