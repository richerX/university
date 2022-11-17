using System;

public class Citizen : IOptimist, IPessimist
{
    int value;

    public Citizen(int predictedValue)
    {
        value = predictedValue;
    }

    public double GetForecast()
    {
        return value * 1.1;
    }

    double IPessimist.GetForecast()
    {
        return value * 0.6;
    }

    double IOptimist.GetForecast()
    {
        return value * 2;
    }

    internal static Citizen Parse(string input)
    {
        int answer;
        if (!int.TryParse(input, out answer))
            throw new ArgumentException("Incorrect citizen");
        return new Citizen(answer);
    }
}