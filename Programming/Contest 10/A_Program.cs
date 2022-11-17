using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        List<ushort> array = new List<ushort>();
        ushort length;

        using (StreamReader reader = new StreamReader(File.Open("input.txt", FileMode.Open, FileAccess.Read)))
        {
            length = ushort.Parse(reader.ReadLine());
            for (int i = 0; i < length; i++)
                array.Add(ushort.Parse(reader.ReadLine()));
        }

        using (BinaryWriter writer = new BinaryWriter(File.Open("output.bin", FileMode.OpenOrCreate, FileAccess.Write)))
        {
            writer.Write(length);
            foreach (var elem in array)
                writer.Write(elem);
        }
    }
}