using System;
using System.Threading;

partial class Program
{
    public static double Pow(double n, int k)
    {
        if (k == 0)
        {
            return 1;
        }
        if (k == 1)
        {
            return n;
        }
        if (k % 2 == 0)
        {
            return Pow(n * n, k / 2);
        }
        else
        {
            return Pow(n, k - 1) * n;
        }
    }

    public static void Main(string[] args)
    {
        double n;
        int k;
        if (!double.TryParse(Console.ReadLine(), out n))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        if (!int.TryParse(Console.ReadLine(), out k))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        if (k < 0)
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        Console.WriteLine(Pow(n, k));
    }
}