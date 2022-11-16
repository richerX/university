using System;
using System.Collections.Generic;
using System.IO;

public class ReadWriter
{
    public static Tuple<char, char> GetMostAndLeastCommonLetters(string path)
    {
        string text;
        using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read)))
            text = reader.ReadToEnd();

        Dictionary<char, int> chars = new Dictionary<char, int>();
        int diff = 'a' - 'A';
        foreach (char elem in text)
        {
            if ('A' <= elem && elem <= 'Z')
            {
                char newElem = (char)(elem + diff);
                if (!chars.ContainsKey(newElem))
                    chars[newElem] = 0; 
                chars[newElem] += 1;
            }

            if ('a' <= elem && elem <= 'z')
            {
                if (!chars.ContainsKey(elem))
                    chars[elem] = 0;
                chars[elem] += 1;
            }
        }

        return new Tuple<char, char>(GetMinimum(chars), GetMaximum(chars));
    }

    public static void ReplaceMostRareLetter(Tuple<char, char> leastAndMostCommon, string inputPath, string outputPath)
    {
        List<string> strings = new List<string>();
        using (StreamReader reader = new StreamReader(File.Open(inputPath, FileMode.Open, FileAccess.Read)))
        {
            while (reader.Peek() > -1)
                strings.Add(reader.ReadLine());
        }

        List<string> answer = new List<string>();
        foreach (var elem in strings)
            answer.Add(UpdateString(leastAndMostCommon, elem));

        using (StreamWriter writer = new StreamWriter(File.Open(outputPath, FileMode.OpenOrCreate, FileAccess.Write)))
        {
            foreach (var elem in answer)
                writer.Write(elem + Environment.NewLine);
        }
    }

    public static string UpdateString(Tuple<char, char> leastAndMostCommon, string initial)
    {
        string answer = "";
        int diff = 'a' - 'A';

        char badChar = leastAndMostCommon.Item1;
        char badChar2 = (char)(leastAndMostCommon.Item1 - diff);
        char goodChar = leastAndMostCommon.Item2;
        char goodChar2 = (char)(leastAndMostCommon.Item2 - diff);

        foreach (char elem in initial)
        {
            if (elem == badChar)
                answer += goodChar;
            else if (elem == badChar2)
                answer += goodChar2;
            else
                answer += elem;
        }

        return answer;
    }

    public static char GetMinimum(Dictionary<char, int> chars)
    {
        char answer = 'a';
        int answerCount = int.MaxValue;
        foreach (var pair in chars)
        {
            if (pair.Value < answerCount)
            {
                answer = pair.Key;
                answerCount = pair.Value;
            }
        }
        return answer;
    }

    public static char GetMaximum(Dictionary<char, int> chars)
    {
        char answer = 'a';
        int answerCount = int.MinValue;
        foreach (var pair in chars)
        {
            if (pair.Value > answerCount)
            {
                answer = pair.Key;
                answerCount = pair.Value;
            }
        }
        return answer;
    }
}