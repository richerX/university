using System;

public class Book : Product
{
    private readonly int age;

    public Book(long id, int age) : base(id)
    {
        this.id = id;
        this.age = age;
    }

    public override string ToString()
    {
        return $"Product. Id = {this.id}. Type = Book. Age = {this.age}";
    }
}