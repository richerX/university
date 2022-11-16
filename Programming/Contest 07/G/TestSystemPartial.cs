using System;
using System.Collections.Generic;

public partial class TestSystem
{
    List<User> users;

    public TestSystem()
    {
        users = new List<User>();
        Notifications += SendNotification;
    }

    public void Add(string username)
    {
        var notifications = new List<string>();
        foreach (var user in users)
        {
            if (user.username == username && user.turnedOff == false)
                throw new ArgumentException("User already exists");
            if (user.username == username && user.turnedOff == true)
                notifications = user.notifications;
        }
        users.Add(new User(username, notifications));
    }
    
    public void Remove(string username)
    {
        foreach (var user in users)
        {
            if (user.username == username && user.turnedOff == false)
            {
                user.turnedOff = true;
                return;
            }
        }
        throw new ArgumentException("User does not exist");
    }

    public void SendNotification(string message)
    {
        foreach (var user in users)
        {
            if (user.turnedOff == false)
            {
                Console.WriteLine(user);
                if (user.notifications.Count > 0)
                {
                    Console.WriteLine("Received notifications:");
                    foreach (var notification in user.notifications)
                        Console.WriteLine(notification);
                }
                Console.WriteLine($"New notification: {message}");
                user.notifications.Add(message);
            }
        }
    }
}