using System;
using System.Collections.Generic;
using System.IO;

public partial class Program
{
    private static Dictionary<string, List<string>> Convert(KeyPredicate keyPredicate, ValuePredicate valuePredicate, Dictionary<int, List<int>> data)
    {
        var answer = new Dictionary<string, List<string>>();
        foreach (var key in data.Keys)
        {
            var answerKey = areas[key];
            var answerValues = new List<string>();
            foreach (var value in data[key])
                answerValues.Add(cities[value]);
            answer.Add(answerKey, answerValues);
        }
        return answer;
    }

    private static void LoadAreas()
    {
        using (StreamReader stream = new StreamReader("areas.txt"))
        {
            while (stream.Peek() != -1)
            {
                var str = stream.ReadLine();
                areas.Add(int.Parse(str.Split()[0]), GetCityName(str));
            }
        }

        /*
        foreach (var str in File.ReadAllLines("areas.txt"))
            areas.Add(int.Parse(str.Split()[0]), str.Split()[1]);
        */
    }

    private static void LoadCities()
    {
        using (StreamReader stream = new StreamReader("cities.txt"))
        {
            while (stream.Peek() != -1)
            {
                var str = stream.ReadLine();
                cities.Add(int.Parse(str.Split()[0]), GetCityName(str));
            }
        }

        /*
        foreach (var str in File.ReadAllLines("cities.txt"))
            cities.Add(int.Parse(str.Split()[0]), str.Split()[1]);
        */
    }

    private static string GetCityName(string str)
    {
        var answer = "";
        var array = str.Split();
        for (int i = 1; i < array.Length; i++)
        {
            answer += array[i] + " ";
        }
        return answer.Substring(0, answer.Length - 1);
    }
}