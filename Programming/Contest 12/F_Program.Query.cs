using System;
using System.Collections.Generic;
using System.Linq;


public partial class Program
{
    private static List<Cat> ChooseCats(int minTailLength, int maxTailLength, int maxAge, List<Cat> cats)
    {
        return (from cat in cats
                where cat.TailLength == cats.Max(x => x.TailLength) ||
                (cat.IsBlack && cat.TailLength >= minTailLength && cat.TailLength <= maxTailLength && cat.Age <= maxAge)
                select cat).ToList();
    }
}