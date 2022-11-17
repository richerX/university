using System;

partial class Program
{
    private static bool TryParseInput(string inputA, string inputB, out int a, out int b)
    {
        if ((!int.TryParse(inputA, out a)) || (!int.TryParse(inputB, out b)) || (a < 0) || (b < 0))
        {
            a = b = 0;
            return false;
        }
        return true;
    }

    public static int GetMaxDigit(int number)
    {
        int answer = number % 10;
        while (number > 0)
        {
            if (number % 10 > answer)
            {
                answer = number % 10;
            }
            number /= 10;
        }
        return answer;
    }

    public static int SwapMaxDigitInNumber(int number, int max_digit, int another_digit)
    {
        int answer = 0;
        int iterator = 1;
        if (number == 0)
        {
            return another_digit;
        }
        while (number > 0)
        {
            if (number % 10 == max_digit)
            {
                answer += iterator * another_digit;
            }
            else
            {
                answer += iterator * (number % 10);
            }
            iterator *= 10;
            number /= 10;
        }
        return answer;
    }

    private static void SwapMaxDigit(ref int a, ref int b)
    {
        int MaxDigitA = GetMaxDigit(a);
        int MaxDigitB = GetMaxDigit(b);
        a = SwapMaxDigitInNumber(a, MaxDigitA, MaxDigitB);
        b = SwapMaxDigitInNumber(b, MaxDigitB, MaxDigitA);
    }
}