using System;

public class Game : Content
{
    private string developerStudio;
    private string genre;
    private short numberOfLevels;

    public Game(int size, string name, string developerStudio, string genre,
        short numberOfLevels) : base(size, name)
    {
        this.size = size;
        this.name = name;
        this.developerStudio = developerStudio;
        this.genre = genre;
        this.numberOfLevels = numberOfLevels;
    }
}