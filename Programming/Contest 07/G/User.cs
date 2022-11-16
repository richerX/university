using System;
using System.Collections.Generic;

public class User
{

    public string username;
    public List<string> notifications;
    public bool turnedOff;

    public User(string username, List<string> notifications)
    {
        this.username = username;
        this.notifications = notifications;
        turnedOff = false;
    }

    public override string ToString() => $"-{username}-";

    public void SendMessage(string text)
    {
        Console.WriteLine("Я не понял зачем этот метод");
    }
    
}