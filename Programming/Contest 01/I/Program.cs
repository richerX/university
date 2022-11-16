using System;

namespace I
{
    class Program
    {
        static void Main(string[] args)
        {
            double number;
            bool Error = false;
            if (!double.TryParse(Console.ReadLine(), out number))
            {
                Error = true;
            };

            if (Error)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                if (number == Math.Floor(number) + 0.5)
                {
                    if (Math.Floor(number) % 2 == 1 | Math.Floor(number) % 2 == -1)
                    {
                        Console.WriteLine(Math.Floor(number));
                    }
                    else
                    {
                        Console.WriteLine(Math.Floor(number) + 1);
                    }
                }
                else
                {
                    Console.WriteLine(Math.Round(number));
                }
            }
        }
    }
}
