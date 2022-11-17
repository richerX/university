using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public partial class Program
{
    private static List<Point> GetPoints()
    {
        var answer = new List<Point>();
        string[] lines = File.ReadAllLines(InputPath);
        foreach (var line in lines)
        {
            int[] coords = Array.ConvertAll(line.Split(), int.Parse);
            answer.Add(new Point(coords[0], coords[1], coords[2]));
        }
        return answer;
    }

    private static HashSet<Point> GetUnique(List<Point> points)
    {
        var answer = new HashSet<Point>();
        foreach (var point in points)
        {
            if (!answer.Contains(point))
                answer.Add(point);
        }
        return answer;
    }
}