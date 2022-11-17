using System;

delegate double Calculate(int n);

class Program
{   
    public static double Sum(int x)
    {
        double answer = 0;
        double current;
        double up;
        for (int i = 1; i <= 5; i++)
        {
            current = 1;
            for (int j = 1; j <= 5; j++)
            {
                up = (i + 42) * x;
                current *= up / j / j;
            }
            answer += current;
        }
        return answer;
    }

    public static void Main(string[] args)
    {
        int x = int.Parse(Console.ReadLine());
        Console.WriteLine($"{Sum(x):f3}");
    }
}