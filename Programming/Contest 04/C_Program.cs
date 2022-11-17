using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        double x = double.Parse(Console.ReadLine());
        int n = 0;
        double current = Math.Pow(x, 4) / 24;
        double totalSum = 0;
        while (Math.Abs(current) > double.Epsilon)
        {
            totalSum += current;
            current = (current * Math.Pow(-x, 3)) / ((3 * n + 5) * (3 * n + 6) * (3 * n + 7));
            n++;
        }
        Console.WriteLine(totalSum);
    }
}