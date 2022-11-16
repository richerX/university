using System;

namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            bool Error = false;

            if (!int.TryParse(Console.ReadLine(), out a))
            {
                Error = true;
            };
            if (a < 0) 
            {
                Error = true;
            }

            if (Error)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                string number_string = a.ToString();
                Console.WriteLine(number_string[number_string.Length - 1]);
            }
        }
    }
}
