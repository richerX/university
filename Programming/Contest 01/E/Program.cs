using System;

namespace E
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
            string string_number = a.ToString();
            if (string_number.Length != 4)
            {
                Error = true;
            }

            if (Error)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                if (string_number[0] == string_number[3] & string_number[1] == string_number[2]) 
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
