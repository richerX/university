using System;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            int b;
            bool Error = false;

            if (!int.TryParse(Console.ReadLine(), out a))
            {
                Error = true;
            };
            if (!int.TryParse(Console.ReadLine(), out b))
            {
                Error = true;
            };

            if (Error)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                Console.WriteLine(a - b);
            }
        }
    }
}
