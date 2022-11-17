using System;
using System.Runtime.Serialization;


[DataContract]
[KnownType(typeof(Meat))]
[KnownType(typeof(Vegetable))]
public class Ingredient : IComparable
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    protected int TimeToCook { get; set; }

    public Ingredient(string name, int timeToCook)
    {
        this.Name = name;
        this.TimeToCook = timeToCook;
    }

    public Ingredient()
    {
    }

    public int CompareTo(Object obj)
    {
        Ingredient ingr = (Ingredient)obj;

        if (this.TimeToCook < ingr.TimeToCook)
            return 1;
        if (this.TimeToCook == ingr.TimeToCook)
            return 0;
        return -1;
    }

    public override string ToString()
    {
        return this.Name;
    }
}