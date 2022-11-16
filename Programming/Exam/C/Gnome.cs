using System;
using System.Collections.Generic;

public class Gnome
{
    private double weight;
    private int height;

    public static Gnome GetGnome(string[] inputLines, double minWeight, int neededHeight)
    {
        var gnomes = new List<Gnome>();
        foreach (var elem in inputLines)
            gnomes.Add(ParseGnome(elem));

        Gnome answer = new Gnome(1, 1);
        foreach (var gnome in gnomes)
            if (gnome.weight > minWeight && neededHeight == gnome.height)
                answer = gnome;
        return answer;
    }

    public static Gnome ParseGnome(string text)
    {
        double w = double.Parse(text.Split()[0]);
        int h = int.Parse(text.Split()[1]);
        return new Gnome(w, h);
    }

    private Gnome(double weight, int height)
    {
        if (weight > 50 || weight <= 0)
            throw new ArgumentException("Incorrect weight");
        if (height >= 100 || height <= 0)
            throw new ArgumentException("Incorrect height");
        this.weight = weight;
        this.height = height;
    }

    public override string ToString()
    {
        return $"{weight, 0:f2} {height}";
    }
}