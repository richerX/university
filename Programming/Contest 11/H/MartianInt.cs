using System;

public class MartianInt
{
    private int value;
    private static int count = 0;
    
    public MartianInt(int value)
    {
        if (value < 0)
            throw new ArgumentException("Value is negative");
        this.value = value;
    }

    public int Value => value;

    public static explicit operator int(MartianInt v)
    {
        count += 1;
        if (v.value + count - 1 < 0)
            throw new ArgumentException("Value is negative");
        return v.value + count - 1;
    }

    public static implicit operator MartianInt(int number)
    {
        count += 1;
        if (number - count + 1 < 0)
            throw new ArgumentException("Value is negative");
        return new MartianInt(number - count + 1);
    }
}