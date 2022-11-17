using System;

public static class IFigureExtensions
{
    public static double GetSquare(this IFigure figure)
    {
        var points = figure.Points;

        if (points.Count == 3)
        {
            double a = GetDistance(points[0], points[2]);
            double h = Math.Abs(points[0].Y - points[1].Y);
            return a * h * 0.5;
        }

        if (points.Count == 4)
        {
            double a = GetDistance(points[0], points[1]);
            double b = GetDistance(points[1], points[2]);
            return a * b;
        }

        return 0;
    }

    public static double GetPerimeter(this IFigure figure)
    {
        var points = figure.Points;

        if (points.Count == 3)
        {
            double a = GetDistance(points[0], points[1]);
            double b = GetDistance(points[1], points[2]);
            double c = GetDistance(points[2], points[0]);
            return a + b + c;
        }

        if (points.Count == 4)
        {
            double a = GetDistance(points[0], points[1]);
            double b = GetDistance(points[1], points[2]);
            return (a + b) * 2;
        }

        return 0;
    }

    public static int CompareByPerimeter(this IFigure firstFigure, IFigure secondFigure)
    {
        if (firstFigure.GetPerimeter() > secondFigure.GetPerimeter())
            return 1;
        if (firstFigure.GetPerimeter() == secondFigure.GetPerimeter())
            return 0;
        return -1;
    }

    public static int CompareBySquare(this IFigure firstFigure, IFigure secondFigure)
    {
        if (firstFigure.GetSquare() > secondFigure.GetSquare())
            return 1;
        if (firstFigure.GetSquare() == secondFigure.GetSquare())
            return 0;
        return -1;
    }

    public static double GetDistance(Point point1, Point point2)
    {
        int x = point1.X - point2.X;
        int y = point1.Y - point2.Y;
        return Math.Sqrt(x * x + y * y);
    }
}