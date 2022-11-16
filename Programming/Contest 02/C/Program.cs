using System;

partial class Program
{
    static void Main(string[] args)
    {
        int a;
        int b;
        if (!int.TryParse(Console.ReadLine(), out a))
        {
            Console.WriteLine("Incorrect input");
            return;
        };
        if (!int.TryParse(Console.ReadLine(), out b))
        {
            Console.WriteLine("Incorrect input");
            return;
        };
        if (a >= b)
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        for (int i = a; i < b; i++)
        {
            if (i % 2 == 0)
            {
                Console.WriteLine(i);
            }
        }
    }
}
