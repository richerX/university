using System;
using System.Collections.Generic;

public class Loader
{
    public static Dictionary<string, int> memory = new Dictionary<string, int>();
    public static int loaded = 0;

    public static void AddValue(string key)
    {
        memory[key]++;
    }

    public static void Download(IList<IDownload> downloadsQueue)
    {
        //memory["Movie"] = 0;
        //memory["Content"] = 0;
        //memory["Track"] = 0;
        //memory["Game"] = 0;

        foreach (var elem in downloadsQueue)
        {
            if (!elem.DownloadContent())
                break;
        }

        Console.WriteLine();
        Console.WriteLine("Downloaded content:");
        foreach (var pair in memory)
            Console.WriteLine($"{pair.Key}: {pair.Value}");

        memory = new Dictionary<string, int>();
        loaded = 0;
    }
}