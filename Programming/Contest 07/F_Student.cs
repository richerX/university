using System;

public class Student
{
    public int grade;
    public string name;

    private Student(string name, int grade)
    {
        this.name = name;
        this.grade = grade;
    }

    public static Student Parse(string line)
    {
        int grade;
        if (!int.TryParse(line.Split()[1], out grade))
            throw new ArgumentException("Incorrect input mark");

        string name = line.Split()[0];
        if (name.Length < 3)
            throw new ArgumentException("Incorrect name");
        if (name[0] < 'A' || name[0] > 'Z')
            throw new ArgumentException("Incorrect name");

        if (grade < 0 || grade > 10)
            throw new ArgumentException("Incorrect mark");

        return new Student(name, grade);
    }

    public override string ToString()
    {
        return $"{name} got a grade of {grade}.";
    }
}