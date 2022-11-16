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

    private static int GetMaxInArray(int[] numberArray)
    {
        int answer = numberArray[0];
        for (int i = 0; i < numberArray.Length; i++)
        {
            if (numberArray[i] > answer)
            {
                answer = numberArray[i];
            }
        }
        return answer;
    }
}