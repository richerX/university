using System;

public class Point
{
    int x, y, z;

    public Point(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        Point p = (Point)obj;
        return x == p.x && y == p.y && z == p.z;
    }

    public override int GetHashCode() => 100 * x + 10 * y + z;

    public override string ToString() => $"{x} {y} {z}";
}