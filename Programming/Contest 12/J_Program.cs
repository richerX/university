using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        string line = Console.ReadLine();
        string elements = "<>/abcdefghijklmnopqrstuvwxyz";
        string assumption;
        bool foundAnswer = false;

        for (int i = 0; i < line.Length; i++)
        {
            foreach (var substitution in elements)
            {
                if (line[i] != substitution)
                {
                    assumption = line.Substring(0, i) + substitution + line.Substring(i + 1);
                    //Console.WriteLine($"{assumption} {Check(assumption)}");
                    if (Check(assumption))
                    {
                        Console.WriteLine(assumption);
                        foundAnswer = true;
                        break;
                    }
                }
            }

            if (foundAnswer)
                break;
        }
    }
    
    public static bool Check(string line)
    {
        int length = line.Length;
        var tags = new List<string>();

        int i = 0;
        int j = 0;
        string tagText;
        int tagsCount = 0;
        while (i < length)
        {
            if (line[i] == '>' || line[i] == '/')
                return false;
            if (line[i] == '<')
            {
                for (int k = i + 1; k < length; k++)
                {
                    if (line[k] == '>')
                    {
                        if (k == i + 1)
                            return false;
                        j = k;
                        break;
                    }
                    if (line[k] == '<')
                        return false;
                    if (k == length - 1)
                        return false;
                }

                //string tag = line.Substring(i, j - i + 1);
                //Console.WriteLine(tag);

                // Закрывающий тег
                if (line[i + 1] == '/')
                {
                    tagText = line.Substring(i + 2, j - i - 2);
                    if (tagText == "")
                        return false;
                    if (tagsCount == 0 || tags[tagsCount - 1] != tagText)
                        return false;
                    tags.RemoveAt(tagsCount - 1);
                    tagsCount -= 1;
                }

                // Открывающий тег
                else
                {
                    tagText = line.Substring(i + 1, j - i - 1);
                    if (tagText == "")
                        return false;
                    tags.Add(tagText);
                    tagsCount += 1;
                }

                i = j;
            }
            i++;
        }

        return tagsCount == 0;
    }

    public static void Print(List<string> array)
    {
        Print(array.ToArray());
    }

    public static void Print(string[] array)
    {
        for (int i = 1; i < array.Length - 1; i++)
            Console.Write($"{array[i]} ");
        Console.WriteLine();
    }
}