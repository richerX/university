using System;

public class Download<T> : IDownload where T : Content
{
    private readonly T download;

    public Download(T download)
    {
        this.download = download;
    }

    public bool DownloadContent()
    {
        if (Loader.loaded + download.size > Program.FreeSpace)
        {
            Console.WriteLine($"{Program.FreeSpace - Loader.loaded}/{download.size} MB");
            Console.WriteLine($"Not enough free space!");
            return false;
        }

        if (download.GetType() == typeof(Movie))
        {
            if (!Loader.memory.ContainsKey("Movie"))
                Loader.memory["Movie"] = 0;
            Loader.memory["Movie"]++;
        }

        if (download.GetType() == typeof(Content))
        {
            if (!Loader.memory.ContainsKey("Content"))
                Loader.memory["Content"] = 0;
            Loader.memory["Content"]++;
        }

        if (download.GetType() == typeof(Track))
        {
            if (!Loader.memory.ContainsKey("Track"))
                Loader.memory["Track"] = 0;
            Loader.memory["Track"]++;
        }

        if (download.GetType() == typeof(Game))
        {
            if (!Loader.memory.ContainsKey("Game"))
                Loader.memory["Game"] = 0;
            Loader.memory["Game"]++;
        }

        Loader.loaded += download.size;
        Console.WriteLine($"{download.size}/{download.size} MB");
        return true;
    }
}