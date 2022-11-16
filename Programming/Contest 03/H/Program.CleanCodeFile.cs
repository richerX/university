using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

partial class Program
{
    private static string[] ReadCodeLines(string codePath)
    {
        return File.ReadAllLines(codePath, Encoding.UTF8);
    }

    private static string[] CleanCode(string[] array)
    {
        bool multistringComment = false;
        List<string> answer = new List<string>();
        foreach (var str in array)
        {
            if (multistringComment)
            {
                if (str.Trim().EndsWith("*/"))
                {
                    multistringComment = false;
                }
                continue;
            }
            if (str.Trim().StartsWith("//"))
            {
                continue;
            }
            if (str.Trim().StartsWith("/*"))
            {
                if (!str.Trim().EndsWith("*/"))
                {
                    multistringComment = true;
                }
                continue;
            }
            answer.Add(str);
        }
        return answer.ToArray();
    }

    private static void WriteCode(string codeFilePath, string[] codeLines)
    {
        File.WriteAllLines(codeFilePath, codeLines);
    }
}