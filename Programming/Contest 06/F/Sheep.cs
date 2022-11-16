
using System;

public class Sheep
{

    public int Id;
    public string Name;
    public string Sound;

    public Sheep(int id, string name, string sound)
    {
        if (id <= 0 || id >= 1000)
            throw new ArgumentException("Incorrect input");
        Id = id;
        Name = name;
        Sound = sound;
    }

    public override string ToString()
    {
        return $"[{Id}-{Name}]: {Sound}...{Sound}";
    }

}
