using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

partial class Program
{
    private static readonly string[] Separators = { " ", ". ", ", ", "? ", "! ", ": ", "; " };

    private static void CountInFile(string filePath, out int linesCount, out int wordsCount, out int charsCount)
    {
        string[] text = File.ReadAllLines(filePath, Encoding.UTF8);
        linesCount = text.Length;
        wordsCount = 0;
        charsCount = 0;
        foreach (var line in text)
        {
            wordsCount += line.Trim().Split().Length;
            charsCount += line.Length;
        }
    }
}