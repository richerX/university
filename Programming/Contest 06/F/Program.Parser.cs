using System;


public partial class Program
{
    static Sheep ParseSheep(string line)
    {
        var array = line.Split(" ");
        return new Sheep(int.Parse(array[4]), array[1], array[6]);
    }
}
