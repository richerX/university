using System;

namespace G
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
            if (number < 0)
            {
                Error = true;
            }

            if (Error)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                int answer = (int)(number * 10) % 10;
                Console.WriteLine(answer);
            }
        }
    }
}
