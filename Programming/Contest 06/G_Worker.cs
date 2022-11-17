using System;
using System.Collections.Generic;

public class Worker
{

    Apple[] apples;

    public Worker(Apple[] apples)
    {
        this.apples = apples;
    }

    public Apple[] GetSortedApples()
    {
        Array.Sort(apples, (x, y) => x.Weight.CompareTo(y.Weight));
        return apples;
    }
}