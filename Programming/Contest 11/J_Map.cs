using System;
using System.Collections.Generic;

public class Map<TKey, TValue>
{
    private List<(TKey, TValue)> map;

    public Map()
    {
        map = new List<(TKey, TValue)>();
    }

    public void Add(TKey key, TValue value)
    {
        foreach (var elem in map)
        {
            if (elem.Item1.GetHashCode() == key.GetHashCode())
                throw new ArgumentException($"An item with the same key has already been added. Key: {key}");
        }
        map.Add((key, value));
    }

    public TValue this[TKey key] => FindValue(key);

    public TValue FindValue(TKey key)
    {
        foreach (var elem in map)
        {
            if (elem.Item1.GetHashCode() == key.GetHashCode())
                return elem.Item2; 
        }
        throw new ArgumentException($"The given key '{key}' was not present in the map.");
        
    }

    public bool Remove(TKey key)
    {
        for (int i = 0; i < map.Count; i++)   
        {
            if (map[i].Item1.GetHashCode() == key.GetHashCode())
            {
                map.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public int Count => map.Count;

    public bool ContainsKey(TKey key)
    {
        foreach (var elem in map)
        {
            if (elem.Item1.GetHashCode() == key.GetHashCode())
                return true;
        }
        return false;
    }
}