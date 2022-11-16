using System;
using System.Xml.Serialization;



public class Ash : Tree
{
    public int leafCount;

    public Ash()
    {
    }

    public Ash(int height, int age, int leafCount) : base(height, age)
    {
        this.height = height;
        this.age = age;
        this.leafCount = leafCount;
    }

    public override string ToString()
    {
        return $"Ash with {base.ToString()} leaf:{leafCount}";
    }
}
