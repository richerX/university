using System;
using System.Collections.Generic;
using System.IO;

public static class Analytics
{
    public static double FindGpa(List<Student> students)
    {
        double allGrades = 0;
        foreach (var student in students)
        {
            double studentGrades = 0;
            foreach (var grade in student.Grades)
                studentGrades += grade;
            allGrades += studentGrades / student.Grades.Count;
        }
        return allGrades / students.Count;
    }


    public static void WriteStudentsWithGpaNoLessThanAverage(List<Student> students, string path, double gpa)
    {
        string text = $"{gpa, 0:f2}" + Environment.NewLine;
        foreach (var student in students)
        {
            double studentGrades = 0;
            foreach (var grade in student.Grades)
                studentGrades += grade;
            double averageGrade = studentGrades / student.Grades.Count;

            if (averageGrade >= gpa)
                text += $"{student.Name} {student.LastName} {student.GroupNumber}" + Environment.NewLine;
        }
        File.WriteAllText(path, text);
    }
}