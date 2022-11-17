using System;

public class Vector : IComparable
{
    int x, y;

    public Vector(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public double Length
    {
        get { return Math.Sqrt(x * x + y * y); }
    }

    internal static Vector Parse(string input)
    {
        string[] array = input.Split();
        int a, b;
        if (array.Length == 2 && int.TryParse(array[0], out a) && int.TryParse(array[1], out b))
            return new Vector(a, b);
        throw new ArgumentException("Incorrect vector");
    }

    public int CompareTo(object second)
    {
        if (Length > ((Vector)second).Length)
            return 1;
        if (Length < ((Vector)second).Length)
            return -1;
        return 0;
    }
}