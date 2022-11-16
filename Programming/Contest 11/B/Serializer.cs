using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Serializer
{
    static List<Student> students = new List<Student>();

    public static void ReadStudents(string path)
    {
        foreach (var line in File.ReadAllLines(path))
            students.Add(Student.Create(line));
    }

    public static void SerializeStudents(string path)
    {
        BinaryFormatter format = new BinaryFormatter();
        using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            format.Serialize(stream, students);
        }
    }
}