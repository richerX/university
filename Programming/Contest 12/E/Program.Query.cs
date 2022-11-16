using System;
using System.Collections.Generic;
using System.Linq;

public partial class Program
{
    private static List<Cat> ChooseCats(int minTailLength, int maxTailLength, int maxAge, List<Cat> cats)
    {
        var array1 = from cat in cats where cat.IsBlack select cat;
        var array2 = from cat in array1 where cat.TailLength >= minTailLength select cat;
        var array3 = from cat in array2 where cat.TailLength <= maxTailLength select cat;
        var array4 = from cat in array3 where cat.Age <= maxAge select cat;
        return array4.ToList();
    }
}