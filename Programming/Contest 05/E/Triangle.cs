using System;
using System.Transactions;

public class Triangle
{
    private readonly Point a;
    private readonly Point b;
    private readonly Point c;

    private double AB => GetLengthOfSide(a, b);
    private double AC => GetLengthOfSide(a, c);
    private double BC => GetLengthOfSide(b, c);

    public Triangle(Point a, Point b, Point c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double GetPerimeter()
    {
        return this.AB + this.AC + this.BC;
    }

    public double GetSquare()
    {
        double p = this.GetPerimeter() / 2;
        double squareSquare = p * (p - this.AB) * (p - this.AC) * (p - this.BC);
        return Math.Sqrt(squareSquare);
    }

    public bool IsEquilateral(out double side, out double basis)
    {
        if (this.AB == this.BC)
        {
            side = this.AB;
            basis = this.AC;
            return true;
        }
        if (this.AB == this.AC)
        {
            side = this.AB;
            basis = this.BC;
            return true;
        }
        if (this.BC == this.AC)
        {
            side = this.BC;
            basis = this.AB;
            return true;
        }
        side = -1;
        basis = -1;
        return false;
    }

    public bool GetAngleBetweenEqualsSides(out double angle)
    {
        double side, basis;
        if (IsEquilateral(out side, out basis))
        {
            double alpha = Math.Acos(basis / (2 * side));
            angle = Math.PI - 2 * alpha;
            return true;
        }
        angle = 0;
        return false;
    }

    public bool GetHypotenuse(out double hypotenuse)
    {
        double line1 = GetSquareLengthOfSide(a, b);
        double line2 = GetSquareLengthOfSide(a, c);
        double line3 = GetSquareLengthOfSide(b, c);
        var array = new double[3] { line1, line2, line3 };
        Array.Sort(array);
        // Console.WriteLine($"{array[0]} {array[1]} {array[2]}");
        if (array[0] + array[1] == array[2])
        {
            hypotenuse = Math.Sqrt(array[2]);
            return true;
        }
        hypotenuse = 0;
        return false;
    }

    private static double GetLengthOfSide(Point first, Point second)
    {
        double sideOne = first.GetX() - second.GetX();
        double sideTwo = first.GetY() - second.GetY();
        return Math.Sqrt(Math.Pow(sideOne, 2) + Math.Pow(sideTwo, 2));
    }

    private static double GetSquareLengthOfSide(Point first, Point second)
    {
        double sideOne = first.GetX() - second.GetX();
        double sideTwo = first.GetY() - second.GetY();
        return Math.Pow(sideOne, 2) + Math.Pow(sideTwo, 2);
    }
}