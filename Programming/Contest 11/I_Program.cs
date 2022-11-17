using System;
using System.Collections.Generic;
using System.IO;


public class Program
{
    public static List<(int, int, double)> GetPoints()
    {
        int n = int.Parse(Console.ReadLine());
        int a, b;
        string[] input;
        List<(int, int, double)> points = new List<(int, int, double)>();
        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine().Split();
            a = int.Parse(input[0]); ;
            b = int.Parse(input[1]);
            points.Add((a, b, GetDistance(a, b)));
        }
        return points;
    }
    
    public static double GetDistance(int a, int b)
    {
        return Math.Sqrt(a * a + b * b);
    }

    public static void Main(string[] args)
    {
        int k = int.Parse(Console.ReadLine());
        List<(int, int, double)> allPoints = GetPoints();

        List<(int, int)> points = new List<(int, int)>();
        allPoints.Sort((x, y) => x.Item3.CompareTo(y.Item3));
        for (int i = 0; i < k; i++)
            points.Add((allPoints[i].Item1, allPoints[i].Item2));

        points.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        points.Sort((x, y) => x.Item1.CompareTo(y.Item1));

        foreach (var elem in points)
            Console.WriteLine($"({elem.Item1}, {elem.Item2})");
    }
}