using System;
using System.IO;

public class Summator
{
    int sum;

    public Summator(string path)
    {
        int answer = 0;
        using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read)))
        {
            int[] array;
            while (reader.Peek() > -1)
            {
                array = Array.ConvertAll(reader.ReadLine().Split("_"), int.Parse);
                foreach (var elem in array)
                    answer += elem;
            }
        }
        sum = answer;

    }

    public int Sum
    {
        get
        {
            return sum;
        }
    }
}