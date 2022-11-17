using System;

public class Parabola : Function
{
    public override double GetValueInX(double x)
    {
        return x * x + x + 7;
    }
}
