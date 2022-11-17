using System;
using System.IO;
using System.Text;

public class BinaryFileReader
{
    private string path;

    public BinaryFileReader(string path)
    {
        this.path = path;
    }

    public int GetDifference()
    {
        int sum1 = 0;
        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read), Encoding.ASCII))
        {
            while (reader.PeekChar() > -1)
                sum1 += reader.ReadInt32();
        }

        int sum2 = 0;
        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read), Encoding.ASCII))
        {
            while (reader.PeekChar() > -1)
                sum2 += reader.ReadInt16();
        }

        return sum1 - sum2;
    }
}