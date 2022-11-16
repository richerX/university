using System;

public abstract class Function
{
    public static Function GetFunction(string functionName)
    {
        if (functionName == "Sin")
            return new Sin();
        else if (functionName == "Exp")
            return new Exponent();
        else if (functionName == "Parabola")
            return new Parabola();
        else
            throw new ArgumentException("Incorrect input");
    }

    public abstract double GetValueInX(double x);

    public static double SolveIntegral(Function func, double left, double right, double step)
    {
        if (left > right)
            throw new ArgumentException("Left border greater than right");
        double answer = 0;
        for (double i = left; i < right; i += step)
            answer += Trapeze(func, i, Math.Min(i + step, right));
        return answer;
    }

    public static double Trapeze(Function func, double left, double right)
    {
        return (func.GetValueInX(left) + func.GetValueInX(right)) * (right - left) / 2;
    }
}
