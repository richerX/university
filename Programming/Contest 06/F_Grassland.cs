using System;
using System.Collections.Generic;

public class Grassland
{
    List<Sheep> sheeps;

    public Grassland(List<Sheep> sheeps)
    {
        this.sheeps = sheeps;
    }

    public List<Sheep> GetEvenSheeps()
    {
        var answer = new List<Sheep>();
        foreach (var sheep in sheeps)
        {
            if (sheep.Id % 2 == 0)
                answer.Add(sheep);
        }
        return answer;
    }

    public List<Sheep> GetOddSheeps()
    {
        var answer = new List<Sheep>();
        foreach (var sheep in sheeps)
        {
            if (sheep.Id % 2 == 1)
                answer.Add(sheep);
        }
        return answer;
    }

    public List<Sheep> GetSheepsByContainsName(string name)
    {
        var answer = new List<Sheep>();
        foreach (var sheep in sheeps)
        {
            if (sheep.Name.Contains(name))
                answer.Add(sheep);
        }
        return answer;
    }
}
