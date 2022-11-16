using System;
using System.Collections.Generic;

[Serializable]
public class Student
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public int GroupNumber { get; private set; }
    public List<int> Grades { get; private set; }

    public Student(string name, string lastName, int groupNumber, List<int> grades)
    {
        Name = name;
        LastName = lastName;
        GroupNumber = groupNumber;
        Grades = grades;
    }

    // Jesse Kenny 193 10 9 9 8 9 6
    public static Student Create(string studentInfo)
    {
        string[] array = studentInfo.Split();
        List<int> grades = new List<int>();
        for (int i = 3; i < array.Length; i++)
            grades.Add(int.Parse(array[i]));
        return new Student(array[0], array[1], int.Parse(array[2]), grades);
    }
}