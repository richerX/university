using System;

public abstract partial class Product
{
    public static Product Parse(string line)
    {
        var array = line.Split(";");

        if (array[0] == "Book")
            return new Book(long.Parse(array[1]), int.Parse(array[2]));

        else if (array[0] == "Notebook")
            return new Notebook(long.Parse(array[1]), array[2]);

        throw new ArgumentException("Incorrect type");
    }
}