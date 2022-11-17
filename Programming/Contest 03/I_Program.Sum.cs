using System;
using System.IO;

partial class Program
{
    private static string GetTextFromFile(string inputPath)
    {
        string str;
        using (var file = new StreamReader(inputPath))
        {
            str = file.ReadToEnd();
        }
        return str;
    }

    private static int GetSumFromText(string text)
    {
        char[] separators = new char[] {'\n', '.', '!', '?', ' ', ','};
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        int number;
        int answer = 0;
        foreach (var elem in words)
        {
            if (int.TryParse(elem, out number))
            {
                answer += number;
            }
        }
        return answer;
    }
}