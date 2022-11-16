using System;

public class Exponent : Function
{
    public override double GetValueInX(double x)
    {
        if (x == 0)
            throw new ArgumentException("Function is not defined in point");
        return Math.Pow(Math.E, 1/x);
    }
}
