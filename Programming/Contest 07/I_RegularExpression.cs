using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class RegularExpression
{
    private string regularExpression;

    public RegularExpression(string expresion)
    {
        regularExpression = expresion;
    }

    public string FindAndReplace(string text, string replace)
    {
        text = UpdateText(text);
        if (replace.Contains(@"\"))
        {
            int index = replace.LastIndexOf(@"\");
            string newReplace = replace.Substring(0, index);
            string number = replace.Substring(index + 1);
            replace = newReplace;
            regularExpression = BracketParse(regularExpression)[int.Parse(number) - 1];
        }

        // Print(text, replace, regularExpression);
        Regex regex = new Regex(regularExpression, RegexOptions.RightToLeft);
        return regex.Replace(text, replace).Replace(@"\", "");
    }

    public string UpdateText(string text)
    {
        // text = text.Replace(@"\", @"\\");
        // text = text.Replace(".", @"\.");
        text = text.Replace("!", @"\!");
        text = text.Replace("?", @"\?");
        text = text.Replace("[", @"\[");
        text = text.Replace("]", @"\]");
        text = text.Replace("(", @"\(");
        text = text.Replace(")", @"\)");
        text = text.Replace("–", @"\–");
        return text;
    }

    public string[] BracketParse(string text)
    {
        var answer = new List<string>();
        foreach (var line in text.Split('('))
        {
            foreach (var line2 in line.Split(')'))
            {
                if (!(line2 == ""))
                    answer.Add(line2);
            }
        }
        return answer.ToArray();
    }

    public void Print(string text, string replace, string regularExpression)
    {
        Console.WriteLine();
        Console.WriteLine($"Text = {text}");
        Console.WriteLine($"Replace = {replace}");
        Console.WriteLine($"RegularExpression = {regularExpression}");
        Console.WriteLine();
    }
}
