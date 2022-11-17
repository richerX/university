using System;

partial class Program
{
    private static void GetLetterDigitCount(string line, out int digitCount, out int letterCount)
    {
        digitCount = letterCount = 0;
        foreach (char letter in line)
        {
            if (('a' <= letter && letter <= 'z') || ('A' <= letter && letter <= 'Z'))
            {
                letterCount += 1;
            }
            if ('0' <= letter && letter <= '9')
            {
                digitCount += 1;
            }
        }
    }
}