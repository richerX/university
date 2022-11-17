using System;

public class IntWrapper
{
    private int number;

    public int Number
    {
        get { return number; }
    }

    public IntWrapper(int number)
    {
        this.number = number;
    }

    public uint FindNumberLength()
    {
        if (number < 0)
            throw new ArgumentException("Number should be non-negative.");
        uint answer = 1;
        while (number > 9)
        {
            number /= 10;
            answer += 1;
        }
        return answer;
    }
}
