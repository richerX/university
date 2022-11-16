using System;

internal struct Student : IComparable<Student>
{
    private int id;
    private int height;
    private int math;
    private int english;
    private int PE;

    public static int students;
    public static int compares;

    public Student(int id, int height, int math, int english, int PE)
    {
        this.id = id;
        this.height = height;
        this.math = math;
        this.english = english;
        this.PE = PE;
    }

    internal static Student Parse(string v)
    {
        students += 1;
        var array = Array.ConvertAll(v.Split(), int.Parse);
        return new Student(array[0], array[1], array[2], array[3], array[4]);
    }

    public override string ToString()
    {
        return $"{id}";
    }

    public int CompareTo(Student second)
    {
        if (compares < students)
        {
            compares += 1;
            if (math > second.math)
                return 1;
            if (math == second.math && english > second.english)
                return 1;
            return -1;
        }

        if (PE > second.PE)
            return 1;
        if (PE == second.PE && height > second.height)
            return 1;
        return -1;
    }
}