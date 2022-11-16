using System;

partial class Program
{
    
    static int Factorial(int value)
    {
        int answer = 1;
        for (int i = value; i > 0; i--)
        {
            answer *= i;
        }
        return answer;
    }

    static bool IsInputNumberCorrect(int number)
    {
        return number >= 0;
    }
}