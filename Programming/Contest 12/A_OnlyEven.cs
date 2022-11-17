using System;
using System.Collections;
using System.Collections.Generic;

public class OnlyEven : IEnumerable<int>
{
    private List<int> array = new List<int>();
    
    public OnlyEven(List<int> numbers)
    {
        foreach (var elem in numbers)
            if (elem % 2 == 0)
                array.Add(elem);
    }

    public IEnumerator GetEnumerator()
    {
        return array.GetEnumerator();
    }

    IEnumerator<int> IEnumerable<int>.GetEnumerator()
    {
        return array.GetEnumerator();
    }
}