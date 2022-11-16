using System;

namespace F
{
    class Program
    {
        static void Main(string[] args)
        {
            double a;
            double b;
            bool Error = false;
            if (!double.TryParse(Console.ReadLine(), out a))
            {
                Error = true;
            };
            if (!double.TryParse(Console.ReadLine(), out b))
            {
                Error = true;
            };
            if (a <= 0 | b <= 0)
            {
                Error = true;
            }

            if (Error)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                double c_square = Math.Pow(a, 2) + Math.Pow(b, 2);
                double c = Math.Sqrt(c_square);
                Console.WriteLine(c);
            }
        }
    }
}
