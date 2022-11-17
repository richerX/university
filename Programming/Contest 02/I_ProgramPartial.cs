using System;
using System.Collections.Generic;
using System.Text;

partial class Program
{

    // Проверка входных чисел на корректность
    static bool Validate(int a)
    {
        return a > 0;
    }

    static int GetPerfectNumber(int a)
    {
        while (true)
        {
            if (IsPerfect(a))
            {
                return a;
            }
            a += 1;
        }
    }

    static bool IsPerfect(int n)
    {
        return n == DivisorsSum(n);
    }

    static int DivisorsSum(int n)
    {
        if (n == 1)
        {
            return 0;
        }
        int answer = 1;
        int ceil = (int)Math.Floor(Math.Sqrt(n));
        for (int i = 2; i <= ceil; i++)
        {
            if (n % i == 0)
            {
                answer += i;
                if (n / i != i)
                {
                    answer += n / i;
                }
            }
        }
        return answer;
    }
}
