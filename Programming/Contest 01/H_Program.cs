using System;

namespace H
{
    class Program
    {
        static void Main(string[] args)
        {
            char letter;
            bool Error = false;
            if (!char.TryParse(Console.ReadLine(), out letter))
            {
                Error = true;
            };
            if (letter < 'a' | letter > 'z')
            {
                Error = true;
            };

            if (Error)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                Console.WriteLine(letter - 'a' + 1);
            }
        }
    }
}
