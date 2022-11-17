using System;
public class ReplacedString
{
    private string replacedString;

    public ReplacedString(string line, string a, string b)
    {
        string nextLine = Replace(line, a, b);
        while (nextLine != line)
        {
            line = nextLine;
            nextLine = Replace(nextLine, a, b);
        }
        replacedString = line;
    }

    public static string Replace(string line, string a, string b)
    {
        int i = 0;
        string answer = "";
        while (i < line.Length)
        {
            if (i < line.Length - a.Length + 1 && line.Substring(i, a.Length) == a)
            {
                answer += b;
                i += a.Length;
            }
            else
            {
                answer += line[i];
                i += 1;
            }
        }
        /*
        Console.WriteLine($"line     -> |{line}|");
        Console.WriteLine($"a        -> |{a}|");
        Console.WriteLine($"b        -> |{b}|");
        Console.WriteLine($"Returned -> |{answer}|");
        Console.WriteLine();
        */
        return answer;
    }

    public override string ToString() => replacedString;
}