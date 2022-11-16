using System;
using System.Xml.Serialization;


[XmlInclude(typeof(Oak))]
[XmlInclude(typeof(Ash))]
public class Tree : IComparable
{
    public int height;
    public int age;

    public Tree()
    {
    }

    public Tree(int height, int age)
    {
        this.height = height;
        this.age = age;
    }

    public override string ToString()
    {
        return $"height:{height} age:{age}";
    }

    public int CompareTo(object obj)
    {
        Tree tree = (Tree)obj;

        if (this.height < tree.height)
            return -1;
        if (this.height == tree.height)
            return 0;
        return 1;
    }
}