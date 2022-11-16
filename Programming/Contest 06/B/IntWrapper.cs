using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class IntWrapper
{
    private int number;

    public IntWrapper(int number)
    {
        this.number = number;
    }

    public int FindNumberLength()
    {
        if (number < 0)
            throw new ArgumentException("Number should be non-negative.");
        return number.ToString().Length;
    }
}
