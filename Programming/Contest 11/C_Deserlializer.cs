using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Deserializer
{
    public static List<Student> DeserializeStudents(string path)
    {
        BinaryFormatter format = new BinaryFormatter();
        using (Stream stream = File.OpenRead(path))
            return (List<Student>)format.Deserialize(stream);
    }
}