using System;

public class RegularPolygon : Polygon
{
    public double side;
    public int numberOfSides;

    public RegularPolygon(double side, int numberOfSides)
    {
        if (side <= 0)
            throw new ArgumentException("Side length value should be greater than zero.");
        if (numberOfSides <= 2)
            throw new ArgumentException("Number of sides value should be greater than 3.");
        this.side = side;
        this.numberOfSides = numberOfSides;
    }

    public override double Perimeter => side * numberOfSides;

    public override double Area => (1.0 / 4) * numberOfSides * side * side * (1.0 / Math.Tan(Math.PI / numberOfSides));

    public override string ToString() => $"side: {side}; numberOfSides: {numberOfSides}; area: {Area:f3}; perimeter: {Perimeter:f3}";
}