using System;

partial class Program
{
    public static bool triangleIsCorrect(int a, int b, int c)
    {
        int maximum = Math.Max(Math.Max(a, b), c);
        if (maximum >= a + b + c - maximum)
        {
            return false;
        }
        if (a <= 0 || b <= 0 || c <= 0)
        {
            return false;
        }
        return true;
    }

    public static void Main(string[] args)
    {
        int a;
        int b;
        int c;
        if (!int.TryParse(Console.ReadLine(), out a))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        if (!int.TryParse(Console.ReadLine(), out b))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        if (!int.TryParse(Console.ReadLine(), out c))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        if (triangleIsCorrect(a, b, c)) 
        {
            Console.WriteLine(a + b + c);
        }
        else
        {
            Console.WriteLine("Incorrect input");
        }
    }
}