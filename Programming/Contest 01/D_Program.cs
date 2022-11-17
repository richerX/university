using System;

namespace D
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
                if (a == b)
                {
                    Console.WriteLine("Equal");
                }
                if (a > b)
                {
                    Console.WriteLine("First");
                }
                if (a < b)
                {
                    Console.WriteLine("Second");
                }
            }
        }
    }
}
