using System;

namespace J
{
    class Program
    {
        static void Main(string[] args)
        {
            double x;
            double y;
            bool Error = false;

            if (!double.TryParse(Console.ReadLine(), out x))
            {
                Error = true;
            };
            if (!double.TryParse(Console.ReadLine(), out y))
            {
                Error = true;
            };

            if (Error)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                if (1 <= Math.Pow(x, 2) + Math.Pow(y, 2) & Math.Pow(x, 2) + Math.Pow(y, 2) <= 2)
                {
                    Console.WriteLine("True");
                }
                else
                {
                    Console.WriteLine("False");
                }
            }
        }
    }
}
