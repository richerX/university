using System;

public class Notebook : Product
{
    private readonly string color;
    
    public Notebook(long id, string color) : base(id)
    {
        this.id = id;
        this.color = color;
    }
    
    public override string ToString()
    {
        return $"Product. Id = {this.id}. Type = Notebook. Color = {this.color}";
    }
}