using System;
using System.Collections.Generic;
using System.Linq;

partial class Program
{
    private static IEnumerable<StudentMark> GetStudentMarks(List<Student> students, List<Work> marks)
    {
        return from mark in marks
               group mark by mark.ID into markGroup
               from student in students
               where student.ID == markGroup.Key
               orderby student.FIO
               let x = ((double) markGroup.Sum(x => x.Mark)) / markGroup.Count()
               select new StudentMark(student.FIO, (int)Math.Round(x, MidpointRounding.AwayFromZero));
    }
}