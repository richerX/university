using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


class RandomProxy
{
    StreamWriter log;
    Dictionary<string, int> users = new Dictionary<string, int>();
    Random random = new Random(1579);

    public RandomProxy(StreamWriter log)
    {
        this.log = log;
    }

    public void Register(string login, int age)
    {
        if (users.ContainsKey(login))
            throw new ArgumentException($"User {login}: login is already registered");
        log.WriteLine($"User {login}: login registered");
        users.Add(login, age);
    }

    public int Next(string login)
    {
        int answer;
        int age = GetAge(login);
        if (age < 20)
            answer = random.Next(0, 1000);
        else
            answer = random.Next(0, int.MaxValue);
        log.WriteLine($"User {login}: generate number {answer}");
        return answer;
    }

    public int Next(string login, int maxValue)
    {
        int answer;
        int age = GetAge(login);
        if (age < 20 && maxValue > 1000)
            throw new ArgumentOutOfRangeException($"User {login}: random bounds out of range");
        answer = random.Next(0, maxValue);
        log.WriteLine($"User {login}: generate number {answer}");
        return answer;
    }

    public int Next(string login, int minValue, int maxValue)
    {
        int answer;
        int age = GetAge(login);
        if (age < 20 && maxValue - minValue > 1000)
            throw new ArgumentOutOfRangeException($"User {login}: random bounds out of range");
        answer = random.Next(minValue, maxValue);
        log.WriteLine($"User {login}: generate number {answer}");
        return answer;
    }

    public int GetAge(string login)
    {
        try
        {
            int age = users[login];
        }
        catch
        {
            throw new ArgumentException($"User {login}: login is not registered");
        }
        return users[login];
    }
}
