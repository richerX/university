using System;
using System.Collections.Generic;
#pragma warning disable

public class ArchaeologicalFind
{

    int age;
    int weight;
    string name;
    int index;
    public static int TotalFindsNumber = 0;

    public ArchaeologicalFind(int age, int weight, string name)
    {
        if (age <= 0)
            throw new ArgumentException("Incorrect age");
        if (weight <= 0)
            throw new ArgumentException("Incorrect weight");
        if (name == "?")
            throw new ArgumentException("Undefined name");
        this.age = age;
        this.weight = weight;
        this.name = name;
    }
    
    public static void AddFind(ICollection<ArchaeologicalFind> finds, ArchaeologicalFind archaeologicalFind)
    {
        bool contains = false;
        foreach (var find in finds)
        {
            if (find.age == archaeologicalFind.age)
                if (find.name == archaeologicalFind.name)
                    if (find.weight == archaeologicalFind.weight)
                        contains = true;
        }
        if (!contains)
            finds.Add(archaeologicalFind);
        archaeologicalFind.index = TotalFindsNumber;
        TotalFindsNumber += 1;
    }

    public override bool Equals(object obj)
    {
        return this == obj;
    }
    
    public override string ToString() => $"{index} {name} {age} {weight}";
}