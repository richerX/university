using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StudentReader : IDisposable, IEnumerable<Student>
{
    List<Student> array = new List<Student>();

    public StudentReader(string path)
    {
        using (StreamReader stream = new StreamReader(path))
        {
            while (stream.Peek() != -1)
            {
                var data = Student.PreprocessStudentData(stream.ReadLine());
                array.Add(new Student(data.Item1, data.Item2));
            }
        }

        /*
        foreach (var text in System.IO.File.ReadAllLines(path))
        {
            var data = Student.PreprocessStudentData(text);
            array.Add(new Student(data.Item1, data.Item2));
        }
        */
    }

    public IEnumerable<Student> GetStudentsWithGreaterGpa(double gpa)
    {
        List<Student> answer = new List<Student>();
        foreach (var elem in array)
        {
            if (elem.Gpa > gpa)
                answer.Add(elem);
        }
        return answer;
    }

    public void Dispose()
    {
        ;
    }

    IEnumerator<Student> IEnumerable<Student>.GetEnumerator()
    {
        return array.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return array.GetEnumerator();
    }
}