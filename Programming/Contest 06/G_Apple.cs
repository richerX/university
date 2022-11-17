using System;

public class Apple
{

    public double weight;
    public string color;

    public double Weight
    {
        get
        {
            return weight;
        }
        set
        {
            if (value <= 0 || value > 1000)
                throw new ArgumentException("Incorrect input");
            weight = value;
        }
    }

    public string Color
    {
        get
        {
            return color;
        }
        set
        {
            if (value.Length > 5 || value[0] > 'Z')
                throw new ArgumentException("Incorrect input");
            color = value;
        }
    }

    public override string ToString()
    {
        return $"{Color} Apple. Weight = {Weight:f2}g.";
    }
}