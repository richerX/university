using System;

public class Player : IPlayer
{
    private readonly string name;
    private readonly int age;
    private readonly int speed;
    private readonly int shooting;

    private Player(string name, int age, int speed, int shooting)
    {
        this.name = name;
        this.age = age;
        this.speed = speed;
        this.shooting = shooting;
    }

    public double Skill => speed * 1.5 + shooting * 2 - 0.5 * (age - 30);

    public static Player Parse(string str)
    {
        var array = str.Split(";");
        if (array[0].Trim() == "")
            throw new ArgumentException("Incorrect input");

        int age, speed, shooting;
        if (!int.TryParse(array[1], out age))
            throw new ArgumentException("Incorrect input");
        if (!int.TryParse(array[2], out speed))
            throw new ArgumentException("Incorrect input");
        if (!int.TryParse(array[3], out shooting))
            throw new ArgumentException("Incorrect input");

        return new Player(array[0], age, speed, shooting);
    }
}