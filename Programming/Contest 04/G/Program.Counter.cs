using System.IO;

public static partial class Program
{
    private static int GetCountCapitalLetters(string inputPath)
    {
        int answer = 0;
        string text = File.ReadAllText(inputPath);
        foreach (var sign in text)
        {
            if ('A' <= sign && sign <= 'Z')
            {
                answer++;
            }
        }
        return answer;
    }

    private static void WriteCount(string outputPath, int count)
    {
        File.WriteAllText(outputPath, count.ToString());
    }
}