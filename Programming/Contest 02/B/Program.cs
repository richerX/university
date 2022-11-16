using System;

class Program
{
    static void Main(string[] args)
    {
        int number;
        int answer = 0;
        do
        {
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Incorrect input");
                return;
            };
            if (number % 2 == 1 || number % 2 == -1)
            {
                answer += number;
            }
        } while (number != 0);
        Console.WriteLine($"{answer:d}");
    }
}
