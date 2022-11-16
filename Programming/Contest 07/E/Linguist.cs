using System;
using System.Collections.Generic;

class Linguist : Editor
{
    string bannedWord;

    private Linguist(string name, int salary, string bannedWord) : base(name, salary)
    {
        this.bannedWord = bannedWord;
    }

    public new string EditHeader(string header)
    {
        string answer = "";
        int i  = 0;
        while (i < header.Length)
        {
            if (i + bannedWord.Length <= header.Length && header.Substring(i, bannedWord.Length) == bannedWord)
                i += bannedWord.Length - 1;
            else
                answer += header[i];
            i += 1;
        }
        return base.EditHeader(answer);
    }

    public static Linguist Parse(string line)
    {
        string[] words = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        if (words.Length != 3 || !int.TryParse(words[1], out int payment))
        {
            throw new ArgumentException("Incorrect input");
        }

        return new Linguist(words[0], payment, words[2]);
    }
}