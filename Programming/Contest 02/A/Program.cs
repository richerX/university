using System;

class Program
{
    static void Main(string[] args)
    {
        uint number;
        uint answer = 0;
        if (!uint.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Incorrect input");
            return;
        };
        while (number != 0)
        {
            answer += number % 10;
            number /= 10;
        }
        Console.WriteLine(answer);
    }
}

