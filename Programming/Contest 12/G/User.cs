using System;

public class User
{
    public long id;
    public string name;
    public ushort age;
    private char[] digits = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};

    /*
    public long Id
    {
        get => throw new NotImplementedException();
        private set
        {
            throw new NotImplementedException();
        }
    }
    
    public string Name
    {
        get => throw new NotImplementedException();
        private set
        {
            throw new NotImplementedException();
        }
    }
    
    public ushort Age
    {
        get => throw new NotImplementedException();
        private set
        {
            throw new NotImplementedException();
        }
    }
    */

    private User(long id, string name, ushort age)
    {
        if (id <= 0)
            throw new ArgumentException("Incorrect input");
        if (age > 128)
            throw new ArgumentException("Incorrect input");
        foreach (var digit in digits)
            if (name.Contains(digit))
                throw new ArgumentException("Incorrect input");
        
        this.id = id;
        this.name = name;
        this.age = age;
    }

    public static User Parse(string str)
    {
        long id;
        ushort age;
        string[] array = str.Split(";");
        if (!long.TryParse(array[0].Trim(), out id))
            throw new ArgumentException("Incorrect input");
        if (!ushort.TryParse(array[2].Trim(), out age))
            throw new ArgumentException("Incorrect input");
        return new User(id, array[1].Trim(), age);
    }

    public override string ToString()
    {
        return $"(ID {this.id}) {this.name} {this.age}";
    }
}