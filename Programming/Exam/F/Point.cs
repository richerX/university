using System;

public struct Point
{
    private readonly int x;
    private readonly int y;

    public int X => x;
    public int Y => y;
    private Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point Parse(string str)
    {
        if (str[0] != '(' || str[str.Length - 1] != ')')
            throw new ArgumentException("Incorrect input");
        var array = str.Substring(1, str.Length - 2).Split(".");

        int x, y;
        if (!int.TryParse(array[0], out x) || !int.TryParse(array[1], out y))
            throw new ArgumentException("Incorrect input");
        return new Point(x, y);
    }
}