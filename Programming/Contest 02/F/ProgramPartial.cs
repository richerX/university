using System;
using System.Collections.Generic;
using System.Text;

partial class Program
{
    static bool Validate(int n)
    {
        return n >= 0;
    }

    static int DivisorsSum(int n)
    {
        if (n == 0 || n == 1)
        {
            return 0;
        }
        int answer = 1;
        int ceil = (int) Math.Floor(Math.Sqrt(n));
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
