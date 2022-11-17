using System;


public class Group
{
    Student[] students;
    int maxGradeIndex = 0;
    int minGradeIndex = 0;

    public Group(Student[] students)
    {
        this.students = students;
        if (students.Length < 5)
            throw new ArgumentException("Incorrect group");
        for (int i = 0; i < students.Length; i++)
        {
            if (students[i].grade > students[maxGradeIndex].grade)
                maxGradeIndex = i;
            if (students[i].grade < students[minGradeIndex].grade)
                minGradeIndex = i;
        }
    }

    public int IndexOfMaxGrade()
    {
        return maxGradeIndex;
    }

    public int IndexOfMinGrade()
    {
        return minGradeIndex;
    }

    public Student this[int index]
    {
        get
        {
            return students[index];
        }
    }
}