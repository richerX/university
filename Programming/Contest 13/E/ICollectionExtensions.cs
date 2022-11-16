using System;
using System.Collections.Generic;


public static class ICollectionExtensions
{
    public static void AddRange<T>(this List<int> array, ICollection<int> collection)
    {
        foreach (var elem in collection)
            array.Add(elem);
    }

    public static void RemoveWhere<T>(this List<int> array, Predicate<int> correct)
    {
        var newArray = new List<int>();
        foreach (var elem in array)
            if (!correct(elem))
                newArray.Add(elem);
        array.Clear();
        foreach (var elem in newArray)
            array.Add(elem);
    }

    public static void RemoveDuplicates<T>(this List<int> array)
    {
        var newArray = new List<int>();
        foreach (var elem in array)
            if (!newArray.Contains(elem))
                newArray.Add(elem);
        array.Clear();
        foreach (var elem in newArray)
            array.Add(elem);
    }

    public static List<int> AllWhere<T>(this List<int> array, Predicate<int> correct)
    {
        var answer = new List<int>();
        foreach (var elem in array)
            if (correct(elem))
                answer.Add(elem);
        return answer;
    }
}
