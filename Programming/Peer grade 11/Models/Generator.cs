using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


public static class Generator
{
    /// <summary>
    /// Поля класса.
    /// </summary>
    private static Random random = new Random();
    private static string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "t", "v", "w", "x" };
    private static string[] vowels = { "a", "e", "i", "o", "u", "y" };
    private static string[] words = File.ReadAllLines("wwwroot/data/words.txt");
    private static string[] names = File.ReadAllLines("wwwroot/data/names.txt");

    private static int numberOfUsers;
    private static int numberOfMessages;

    /// <summary>
    /// Создание рандомных пользователей.
    /// </summary>
    /// <returns></returns>
    public static List<User> GenerateUsers()
    {
        var answer = new List<User>();
        numberOfUsers = random.Next(20, 50);
        for (int i = 1; i < numberOfUsers + 1; i++)
        {
            string fullName = GenerateFullName();
            string email = fullName.Split()[1].ToLower() + "_" + GenerateWord() + "@gmail.com";
            answer.Add(new User(i, fullName, email));
        }
        answer.Sort((x, y) => x.Email.CompareTo(y.Email));
        return answer;
    }

    /// <summary>
    /// Создание рандомных сообщений.
    /// </summary>
    /// <param name="users"></param>
    /// <returns></returns>
    public static List<Message> GenerateMessages(List<User> users)
    {
        var answer = new List<Message>();
        numberOfMessages = random.Next(20, 100);
        for (int i = 1; i < numberOfMessages + 1; i++)
        {
            User sender = users[random.Next(0, users.Count)];
            User receiver = users[random.Next(0, users.Count)];
            string subject = GenerateSubject();
            string text = GenerateText();
            answer.Add(new Message(i, sender, receiver, subject, text));
        }
        return answer;
    }

    /// <summary>
    /// Создание рандомного слова.
    /// </summary>
    /// <returns></returns>
    private static string GenerateWord()
    {
        return words[random.Next(0, words.Length)].ToLower();
    }

    /// <summary>
    /// Создание рандомного слова с большой буквы.
    /// </summary>
    /// <returns></returns>
    private static string GenerateCapitalizedWord()
    {
        var word = words[random.Next(0, words.Length)];
        return char.ToUpper(word[0]) + word.Substring(1);
    }

    /// <summary>
    /// Создание рандомного полного имени.
    /// </summary>
    /// <returns></returns>
    private static string GenerateFullName()
    {
        var name = names[random.Next(0, names.Length)];
        var family = "";
        while (family.Length < 5)
            family = GenerateCapitalizedWord();
        return name + " " + family;
    }

    /// <summary>
    /// Создание рандомного текста письма.
    /// </summary>
    /// <returns></returns>
    private static string GenerateText()
    {
        string text = "";
        int length = random.Next(5, 200);
        bool upper = true;
        for (int i = 0; i < length; i++)
        {
            if (upper)
            {
                text += GenerateCapitalizedWord() + " ";
                upper = false;
            }
            else
            {
                text += GenerateWord() + " ";
            }

            if (random.Next(1, 101) <= 10)
            {
                text = text.Trim();
                text += ". ";
                upper = true;
            }
            else if (random.Next(1, 101) <= 10)
            {
                text = text.Trim();
                text += ", ";
            }
        }
        text = text.Trim();
        text = text.Trim('.');
        text = text.Trim(',');
        return text.Trim() + ".";
    }

    /// <summary>
    /// Создание рандомной темы письма.
    /// </summary>
    /// <returns></returns>
    private static string GenerateSubject()
    {
        string text = "";
        int length = random.Next(1, 7);
        text += GenerateCapitalizedWord() + " ";
        for (int i = 0; i < length; i++)
        {
            text += GenerateWord() + " ";
        }
        return text.Trim();
    }
}
