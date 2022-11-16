using System;

partial class Program
{
    private static int[] ParseInput(string input)
    {
        var array = input.Split();
        int[] answer = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            answer[i] = int.Parse(array[i]);
        }
        return answer;
    }

    private static int GetNumberOfEqualElements(int[] first, int[] second)
    {
        int answer = 0;
        for (int i = 0; i < first.Length; i++)
        {
            for (int j = 0; j < second.Length; j++)
            {
                if (first[i] == second[j])
                {
                    answer += 1;
                    break;
                }
            }
        }
        return answer;
    }
}