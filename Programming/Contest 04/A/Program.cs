using System;

class Program
{
    public static void Main(string[] args)
    {
        short a;
        short b;
        if (!short.TryParse(Console.ReadLine(), out a))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        if (!short.TryParse(Console.ReadLine(), out b))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        Console.WriteLine(a ^ b);
    }
}