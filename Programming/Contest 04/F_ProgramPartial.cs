using System;
using System.Collections.Generic;
using System.Text;

partial class Program
{
    static int Count(int[] arr, int k)
    {
        int answer = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] + arr[j] == k)
                {
                    answer += 1;
                }
            }
        }
        return answer;
    }
}
